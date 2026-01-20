using System.Collections.Generic;
using Godot;

public partial class CardDisplay : Node {
    [Export]
    private PackedScene _cardPrefab;

    private Card? _hoveredCard = null;
    private Card? _previousCard = null;
    private List<Card> _cards = [];

    public override void _Ready() {
        _TestSpawnCards();
    }

    private void _TestSpawnCards() {
        for (int i = 0; i < 3; i++) {
            Card card = (Card)_cardPrefab.Instantiate();
            AddChild(card);
            card.Position = new Vector2(i * 100, 0) + new Vector2(600, 300);
            card.Hovered += _HandleHover;
            _cards.Add(card);
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