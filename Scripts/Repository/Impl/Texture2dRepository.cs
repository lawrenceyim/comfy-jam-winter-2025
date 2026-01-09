using Godot;
using Godot.Collections;
using RepositorySystem;

public partial class Texture2dRepository : Node, IAutoload, IRepository {
    public static string AutoloadPath { get; } = "/root/Texture2dRepository";

    [Export]
    private Dictionary<Texture2dId, Texture2D> _textures;

    public Texture2D GetTexture(Texture2dId id) {
        return _textures[id];
    }
}

public enum Texture2dId {
    
}