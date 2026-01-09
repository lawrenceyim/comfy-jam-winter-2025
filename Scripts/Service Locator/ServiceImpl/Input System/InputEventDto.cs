using Godot;

namespace InputSystem;

public abstract class InputEventDto { }

public class KeyDto(string identifier, bool pressed) : InputEventDto {
    public readonly string Identifier = identifier;
    public readonly bool Pressed = pressed;
}

public class MouseMotionDto(Vector2 position, Vector2 relative) : InputEventDto {
    public readonly Vector2 Position = position;
    public readonly Vector2 Relative = relative;
}

public class MouseButtonPressedDto(string identifier, Vector2 position) : InputEventDto {
    public readonly string Identifier = identifier;
    public readonly Vector2 Position = position;
}

public class MouseButtonReleasedDto(string identifier, Vector2 position) : InputEventDto {
    public readonly string Identifier = identifier;
    public readonly Vector2 Position = position;
}