using System.Collections.Generic;

public static class RecipeInfo {
    private static readonly Dictionary<RecipeName, Recipe> _recipes = new() {
        {
            RecipeName.Mistake, new Recipe(RecipeName.Mistake, TextureId.Dish, new Dictionary<IngredientName, int>() {
                { IngredientName.Egg, 2 }
            })
        }
    };

    public static Recipe GetRecipe(RecipeName recipeName) {
        return _recipes[recipeName];
    }

    public static RecipeName FindRecipeByIngredients(Dictionary<IngredientName, int> ingredients) {
        foreach (KeyValuePair<RecipeName, Recipe> recipe in _recipes) {
            bool match = true;
            Dictionary<IngredientName, int> recipeIngredients = recipe.Value.Ingredients;

            if (ingredients.Count != recipeIngredients.Count) {
                continue;
            }

            foreach (KeyValuePair<IngredientName, int> ingredient in ingredients) {
                if (
                    !recipeIngredients.ContainsKey(ingredient.Key) ||
                    recipeIngredients[ingredient.Key] != ingredient.Value
                ) {
                    match = false;
                    break;
                }
            }

            if (match) {
                return recipe.Key;
            }
        }

        return RecipeName.Mistake;
    }
}

public record Recipe(RecipeName Name, TextureId TextureId, Dictionary<IngredientName, int> Ingredients);

public enum RecipeName {
    Mistake = 0,
    ValidDish1 = 1,
}