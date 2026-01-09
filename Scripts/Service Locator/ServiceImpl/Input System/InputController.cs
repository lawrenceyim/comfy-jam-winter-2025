using System;
using Godot;

namespace InputSystem;

public partial class InputController : Node {
    public event Action<InputEventDto> InputFromPlayer;

    public override void _Input(InputEvent @event) {
        switch (@event) {
            case InputEventKey key:
                if (key.Echo) {
                    return;
                }

                InputFromPlayer?.Invoke(new KeyDto(
                    OS.GetKeycodeString(key.PhysicalKeycode),
                    key.Pressed
                ));
                break;
            case InputEventMouseButton mouseButtonPressed:
                InputFromPlayer?.Invoke(new MouseButtonPressedDto(
                    mouseButtonPressed.ButtonIndex.ToString(),
                    GetWindow().GetMousePosition()
                ));
                break;
            case InputEventMouseMotion mouseMotion:
                InputFromPlayer?.Invoke(new MouseMotionDto(
                    mouseMotion.Position,
                    mouseMotion.Relative
                ));
                break;
            case InputEventJoypadButton:
                break;
            case InputEventJoypadMotion:
                break;
        }
    }
}