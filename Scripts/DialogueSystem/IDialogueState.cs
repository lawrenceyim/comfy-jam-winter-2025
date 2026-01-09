namespace DialogueSystem;

public interface IDialogueState {
    public DialogueTreeNode? Evaluate();
    public DialogueTreeNode? FindNode(int key);
}
