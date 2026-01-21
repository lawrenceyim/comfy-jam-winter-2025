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
	Noodle = 11,
	Onion = 12,
	Pasta = 13,
	Pepper = 14,
	Potato = 15,
	Salt = 16,
	Tomato = 17,

	// Recipes that are ingredients
	Mistake = 30_000,
	ChickenNoodleSoup = 30_001,
	FrenchFries = 30_002,
	FriedEgg = 30_003,
	GrilledCheese = 30_004,
	Pancakes = 30_005,
	Ramen = 30_006,
	SteakFrite = 30_007,
	TomatoBisque = 30_008,

	FriedChicken = 30_009,
	Pizza = 30_010,


	// Misc.
	UnknownRecipe = 40_001,
	UnknownIngredient = 40_002,
}
