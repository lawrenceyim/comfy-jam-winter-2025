using System.Collections.Generic;
using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class CardDisplay : Node {
    [Export]
    private PackedScene _cardPrefab;

    private TextureRepository _textureRepository;

    private Card? _hoveredCard = null;
    private Card? _previousCard = null;
    private List<Card> _cards = [];

    private int _cardWidth = 186;
    private int _xOffset = 20;
    private int _xCenter = 640;
    private int _ySpawn = 300;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);
        _TestSpawnCards();
    }

    private void _DisplayCards(List<IngredientName> ingredients) {
        int totalWidth = ingredients.Count * _cardWidth + (ingredients.Count - 1) * _xOffset;
        int xStart = _xCenter - totalWidth / 2;

        for (int i = 0; i < ingredients.Count; i++) {
            Card card = (Card)_cardPrefab.Instantiate();
            card.SetTexture(_textureRepository.GetTexture(IngredientUtil.GetTextureId(ingredients[i])));
            AddChild(card);
            int x = xStart + (i - 1) * (_xOffset + _cardWidth);
            card.Position = new Vector2(x, _ySpawn);
            card.Hovered += _HandleHover;
            _cards.Add(card);
        }
    }

    private void _TestSpawnCards() {
        _DisplayCards(new List<IngredientName>() { IngredientName.Bread, IngredientName.Beef, IngredientName.Beef });
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