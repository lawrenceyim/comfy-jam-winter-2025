using System.Collections.Generic;

namespace DialogueSystem;

public class DialogueTree : IDialogueState {
    private Dictionary<int, DialogueTreeNode> _nodes = new Dictionary<int, DialogueTreeNode>();

    public DialogueTreeNode? Evaluate() {
        DialogueTreeNode? node = _nodes[0]; // Root key should always be 0

        while (node is ConditionNode conditionNode) {
            int key = conditionNode.Condition();

            // DialogueTreeNode key of -1 is end of path
            if (key == -1) {
                return null;
            }

            node = FindNode(key);
        }

        return node;
    }

    public void AddNode(int key, DialogueTreeNode node) {
        _nodes.Add(key, node);
    }

    public DialogueTreeNode? FindNode(int key) {
        return _nodes.GetValueOrDefault(key, null);
    }
}