using System.Collections.Generic;

namespace DialogueSystem;

public class TextRepository {
    private readonly Dictionary<int, string> _texts = new Dictionary<int, string>();

    public TextRepository() { }

    public void AddText(int key, string text) {
        _texts.Add(key, text);
    }

    public string GetText(int key) {
        return _texts.GetValueOrDefault(key, string.Empty);
    }
}