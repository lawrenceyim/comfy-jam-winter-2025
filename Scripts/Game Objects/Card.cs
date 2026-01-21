using System;
using Godot;

public partial class Card : Node2D {
	public event Action<Card, bool> Hovered;

	[Export]
	private Sprite2D _cardSprite;

	[Export]
	private Area2D _hitbox;

	private int _hoveredZIndex = 5;
	private int _regularZIndex = 1;
	private Vector2 _hoveredScale = new(.06f, .06f);
	private Vector2 _regularScale = new(.05f, .05f);

	public override void _Ready() {
		_hitbox.MouseEntered += () => Hovered?.Invoke(this, true);
		_hitbox.MouseExited += () => Hovered?.Invoke(this, false);
		Scale = _regularScale;
	}

	public void SetTexture(Texture2D texture) {
		_cardSprite.Texture = texture;
	}

	public void HoverEffect(bool enable) {
		ZIndex = enable ? +_hoveredZIndex : _regularZIndex;
		Scale = enable ? _hoveredScale : _regularScale;
	}
}
