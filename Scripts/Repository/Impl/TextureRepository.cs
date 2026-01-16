using Godot;
using Godot.Collections;
using RepositorySystem;

public partial class TextureRepository : Node, IAutoload, IRepository {
    public static string AutoloadPath => "/root/TextureRepository";

    [Export]
    private Dictionary<TextureId, Texture2D> _textures;

    public Texture2D GetTexture(TextureId id) {
        return _textures[id];
    }
}

public enum TextureId {
    // Ingredients
    Beef = 1,
    Bread = 2,
    Butter = 3,
    Carrots = 4,
    Cheese = 5,
    Chicken = 6,
    Egg = 7,
    Flour = 8,
    Garlic = 9,
    Milk = 10,
    Onion = 11,
    Pasta = 12,
    Pepper = 13,
    Potato = 14,
    Salt = 15,
    Tomato = 16,

    // Recipes that are ingredients
    Mistake = 30_000,
    ChickenNoodleSoup = 30_001,
    FrenchFries = 30_002,
    FriedEgg = 30_003,
    GrilledCheese = 30_004,
    Pancakes = 30_005,
    SteakFrite = 30_006,
    TomatoBisque = 30_007,
}