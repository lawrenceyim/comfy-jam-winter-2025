using System;

namespace DialogueSystem;

// Use ID of -1 to indicate end of dialogue tree path
public abstract class DialogueTreeNode { }

public class TextNode : DialogueTreeNode {
    public int NextNodeId { get; }
    public int TextId { get; }

    public TextNode() { }

    public TextNode(int textId, int nextNodeId) {
        TextId = textId;
        NextNodeId = nextNodeId;
    }
}

public class QuestionNode : DialogueTreeNode {
    public int QuestionTextId { get; }
    public int[] NextNodesId { get; }
    public int[] AnswerTextsId { get; }

    public QuestionNode() { }

    public QuestionNode(int questionTextId, int[] nextNodesId, int[] answerTextsId) {
        QuestionTextId = questionTextId;
        NextNodesId = nextNodesId;
        AnswerTextsId = answerTextsId;
    }
}

// The Condition should return an int ID for the next node
public class ConditionNode : DialogueTreeNode {
    public Func<int> Condition { get; }

    public ConditionNode() { }

    public ConditionNode(Func<int> condition) {
        Condition = condition;
    }
}

// The Rpc Action should perform an action like adding player gold, flipping a bool, etc.
public class RpcNode : DialogueTreeNode {
    public Action Rpc { get; }
    public int NextNodeId { get; }

    public RpcNode() { }

    public RpcNode(Action rpc, int nextNodeId) {
        Rpc = rpc;
        NextNodeId = nextNodeId;
    }
}