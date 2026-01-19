using System.Collections.Generic;
using Godot;

public partial class CardDisplay : Node {
    [Export]
    private PackedScene _cardPrefab;

    private Card? _hoveredCard = null;
    private List<Card> _cards = [];

    public override void _Ready() {
        _TestSpawnCards();
    }

    private void _TestSpawnCards() {
        for (int i = 0; i < 3; i++) {
            Card card = (Card)_cardPrefab.Instantiate();
            AddChild(card);
            card.Position = new Vector2(i * 100, 0) + new Vector2(600, 500);
            card.Hovered += _HandleHover;
            _cards.Add(card);
        }
    }

    private void _HandleHover(Card card, bool hovered) {
        if (!hovered) {
            card.HoverEffect(false);
            if (_hoveredCard == card) {
                _hoveredCard = null;
            }
        } else {
            _hoveredCard?.HoverEffect(false);
            _hoveredCard = card;
            _hoveredCard?.HoverEffect(true);
        }
    }
}