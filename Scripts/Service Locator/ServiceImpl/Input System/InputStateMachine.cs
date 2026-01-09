using Godot;
using ServiceSystem;

namespace InputSystem;

public partial class InputStateMachine : Node, IService {
    private IInputState _state;
    private InputController _controller;

    public override void _Ready() {
        _controller = new InputController();
        AddChild(_controller);
        _controller.InputFromPlayer += ProcessInput;
    }

    public void SetState(IInputState state) {
        _state = state;
    }

    private void ProcessInput(InputEventDto dto) {
        _state?.ProcessInput(dto);
    }
}