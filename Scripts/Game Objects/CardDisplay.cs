using System;
using System.Collections.Generic;
using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class CardDisplay : Node {
    [Export]
    private PackedScene _cardPrefab;

    private TextureRepository _textureRepository;

    private static IngredientName[] Ingredients = IngredientUtil.GetIngredientNames();
    private Card[] _cards = new Card[Ingredients.Length];
    private bool[] _selectedCards = new bool[Ingredients.Length];
    private Dictionary<Card, int> _cardIndex = new Dictionary<Card, int>();
    List<Card> _hoveredCards = [];

    private const int CardBaseZIndex = 10;
    private const int HoverZIndexBoost = 10;
    private int _cardWidth = 93;
    private int _xOffset = -25;
    private int _xCenter = 640;
    private int _ySpawn = 400;
    private Card _cardHovered = null;


    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);
        _InitIngredients();
    }

    public void ClearCards() {
        for (int i = 0; i < _cards.Length; i++) {
            _cards[i].Visible = false;
        }
    }

    public void _InitIngredients() {
        int totalWidth = Ingredients.Length * _cardWidth + (Ingredients.Length - 1) * _xOffset;
        int xStart = _xCenter - totalWidth / 2;

        for (int i = 0; i < Ingredients.Length; i++) {
            Card card = (Card)_cardPrefab.Instantiate();
            card.SetTexture(_textureRepository.GetTexture(IngredientUtil.GetTextureId(Ingredients[i])));
            _cardIndex.Add(card, i);
            _cards[i] = card;
            AddChild(card);
            int x = xStart + i * (_xOffset + _cardWidth);
            card.Position = new Vector2(x, _ySpawn);
            card.Hovered += _HandleHover;
            card.SetZIndex(i + CardBaseZIndex);
        }
    }

    private void _HandleHover(Card card, bool hovered) {
        if (hovered) {
            _hoveredCards.Add(card);
            if (_cardHovered == null) {
                _cardHovered = card;
                _HoverEffect(card, true);
            }
        } else {
            _hoveredCards.Remove(card);
            _HoverEffect(card, false);
            _cardHovered = null;

            if (_hoveredCards.Count > 0) {
                _cardHovered = _hoveredCards[^1];
                _HoverEffect(_cardHovered, true);
            }
        }
    }

    private void _HoverEffect(Card card, bool hovered) {
        if (hovered) {
            card.HoverEffect(true);
            card.ZIndex = CardBaseZIndex + _cardIndex[card] + HoverZIndexBoost;
        } else {
            card.HoverEffect(false);
            card.ZIndex = CardBaseZIndex + _cardIndex[card];
        }
    }
}