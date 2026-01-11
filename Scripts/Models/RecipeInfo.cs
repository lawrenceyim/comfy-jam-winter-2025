using System.Collections.Generic;

public static class RecipeInfo {
    private static readonly Dictionary<RecipeName, Recipe> _recipes = new() {
        {
            RecipeName.Dish, new Recipe(RecipeName.Dish, TextureId.Dish, new Dictionary<IngredientName, int>() {
                { IngredientName.Egg, 2 }
            })
        }
    };

    public static Recipe GetRecipe(RecipeName recipeName) {
        return _recipes[recipeName];
    }
}

public record Recipe(RecipeName Name, TextureId TextureId, Dictionary<IngredientName, int> Ingredients);

public enum RecipeName {
    Dish
}