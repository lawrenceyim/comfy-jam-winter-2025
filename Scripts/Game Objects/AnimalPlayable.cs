using Godot;

public partial class AnimalPlayable : CharacterBody2D {
    [Export]
    private AnimatedSprite2D _sprite;

    private enum PlayerFacing {
        Up,
        Down,
        Left,
        Right,
    }

    private PlayerFacing _playerFacing = PlayerFacing.Down;

    private const string IdleDown = "Idle Down";
    private const string IdleSide = "Idle Side";
    private const string IdleUp = "Idle Up";
    private const string WalkDown = "Walk Down";
    private const string WalkSide = "Walk Side";
    private const string WalkUp = "Walk Up";

    private float _speed = 250f;
    private Vector2 _movement = Vector2.Zero;

    public override void _Process(double delta) {
        _Move(delta);
        _PlayAnimation();
    }

    private void _Move(double delta) {
        _movement = Vector2.Zero;

        if (Input.IsKeyPressed(Key.W)) {
            _movement.Y -= 1;
        }

        if (Input.IsKeyPressed(Key.S)) {
            _movement.Y += 1;
        }

        if (Input.IsKeyPressed(Key.A)) {
            _movement.X -= 1;
        }

        if (Input.IsKeyPressed(Key.D)) {
            _movement.X += 1;
        }

        _movement = _movement.Normalized();
        MoveAndCollide(_movement * (float)delta * _speed);
    }

    private void _PlayAnimation() {
        if (_movement == Vector2.Zero) {
            _sprite.Play(_playerFacing switch {
                PlayerFacing.Up => IdleUp,
                PlayerFacing.Down => IdleDown,
                PlayerFacing.Left => IdleSide,
                PlayerFacing.Right => IdleSide,
                _ => IdleDown
            });
            return;
        }

        if (_movement.X != 0) {
            _sprite.FlipH = _movement.X < 0;
            _sprite.Play(WalkSide);
        } else {
            _sprite.Play(_movement.Y > 0 ? WalkDown : WalkUp);
        }
    }
}