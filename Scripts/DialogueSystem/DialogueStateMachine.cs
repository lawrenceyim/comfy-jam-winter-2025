using System.Collections.Generic;

namespace DialogueSystem;

public class DialogueStateMachine {
    private readonly Dictionary<int, IDialogueState> _states = new Dictionary<int, IDialogueState>();
    private IDialogueState? _currentState;

    public DialogueTreeNode? Evaluate() {
        _currentState ??= FindState(0);
        return _currentState?.Evaluate();
    }

    public DialogueTreeNode? FindNode(int id) {
        _currentState ??= FindState(0);
        return _currentState?.FindNode(id);
    }

    public void SwitchState(int key) {
        _currentState = FindState(key);
    }

    public void AddState(int key, IDialogueState state) {
        _states.Add(key, state);
    }

    public IDialogueState? FindState(int key) {
        return _states.GetValueOrDefault(key, null);
    }
}