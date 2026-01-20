using System.Collections.Generic;

public static class IngredientInfo {
    private static readonly Dictionary<IngredientName, Ingredient> _ingredients = new() {
        { IngredientName.Egg, new Ingredient(IngredientName.Egg, 1, TextureId.Egg) }
    };

    public static Ingredient GetIngredient(IngredientName ingredientName) {
        return _ingredients[ingredientName];
    }
}

public record Ingredient(IngredientName Name, int Cost, TextureId TextureId);

public enum IngredientName {
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

    // Ingredients that are also recipes
    Mistake = 10_000,
    ChickenNoodleSoup = 10_001,
    FrenchFries = 10_002,
    FriedEgg = 10_003,
    GrilledCheese = 10_004,
    Pancakes = 10_005,
    Ramen = 10_006,
    SteakFrite = 10_007,
    TomatoBisque = 10_008,
}