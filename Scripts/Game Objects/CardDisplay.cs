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
    private Card? _hoveredCard = null;
    private Card? _previousCard = null;
    private Card[] _cards = new Card[Ingredients.Length];
    private bool[] _selectedCards = new bool[Ingredients.Length];

    private int _cardWidth = 93;
    private int _xOffset = -25;
    private int _xCenter = 640;
    private int _ySpawn = 400;

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
            AddChild(card);
            int x = xStart + i * (_xOffset + _cardWidth);
            card.Position = new Vector2(x, _ySpawn);
            card.Hovered += _HandleHover;
        }
    }

    private void _HandleHover(Card card, bool hovered) {
        if (!hovered) {
            card.HoverEffect(false);
            if (_hoveredCard == card) {
                _hoveredCard = null;
                if (_previousCard != null) {
                    _hoveredCard = _previousCard;
                    _previousCard = null;
                    _hoveredCard.HoverEffect(true);
                }
            }

            if (_previousCard == card) {
                _previousCard = null;
            }
        } else {
            _hoveredCard?.HoverEffect(false);
            _previousCard = _hoveredCard;
            _hoveredCard = card;
            _hoveredCard?.HoverEffect(true);
        }
    }
}