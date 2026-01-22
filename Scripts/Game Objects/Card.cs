using System;
using Godot;

public partial class Card : Node2D {
	public event Action<Card, bool> Hovered;
	public IngredientName IngredientName { get; set; }

	[Export]
	private Sprite2D _cardSprite;

	[Export]
	private Area2D _hitbox;

	[Export]
	private CollisionShape2D _collisionShape;

	private Vector2 _hoveredScale = new(.11f, .11f);
	private Vector2 _regularScale = new(.1f, .1f);

	public override void _Ready() {
		_hitbox.MouseEntered += () => Hovered?.Invoke(this, true);
		_hitbox.MouseExited += () => Hovered?.Invoke(this, false);
		Scale = _regularScale;
	}

	public void SetTexture(Texture2D texture) {
		_cardSprite.Texture = texture;
	}

	public void HoverEffect(bool enable) {
		Scale = enable ? _hoveredScale : _regularScale;
	}

	public void EnableHitBox(bool enable) {
		_collisionShape.Disabled = !enable;
	}
}
