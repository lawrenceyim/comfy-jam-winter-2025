using System;
using Godot;

public partial class Card : Sprite2D {
    public event Action<Card, bool> Hovered;

    [Export]
    private Area2D _hitbox;

    private int _hoveredZIndex = 5;
    private int _regularZIndex = 1;
    private Vector2 _hoveredScale = new(1.5f, 1.5f);
    private Vector2 _regularScale = new(1f, 1f);

    public override void _Ready() {
        _hitbox.MouseEntered += () => Hovered?.Invoke(this, true);
        _hitbox.MouseExited += () => Hovered?.Invoke(this, false);
    }

    public void HoverEffect(bool enable) {
        ZIndex = enable ? +_hoveredZIndex : _regularZIndex;
        Scale = enable ? _hoveredScale : _regularScale;
    }
}