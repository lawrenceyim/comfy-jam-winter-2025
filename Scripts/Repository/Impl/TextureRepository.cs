using Godot;
using Godot.Collections;
using RepositorySystem;

public partial class TextureRepository : Node, IAutoload, IRepository {
    public static string AutoloadPath { get; } = "/root/TextureRepository";

    [Export]
    private Dictionary<TextureId, Texture2D> _textures;

    public Texture2D GetTexture(TextureId id) {
        return _textures[id];
    }
}

public enum TextureId {
    // Ingredients
    Egg = 20_001,
    
    // Recipes that are ingredients
    Mistake =  30_000,
    Bread = 30_001,
    
}