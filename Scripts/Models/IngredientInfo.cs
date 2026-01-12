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
    Egg = 0,
    Butter = 1,
    Flour = 2,
    Milk = 3
}