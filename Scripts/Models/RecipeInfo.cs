using System.Collections.Generic;

// no art for ramen. make sure it doesn't get picked
public static class RecipeInfo {
    private static readonly Dictionary<RecipeName, Recipe> _recipes = new() {
        {
            RecipeName.Mistake, new Recipe(
                RecipeName.Mistake,
                TextureId.Mistake,
                new Dictionary<IngredientName, int>() { },
                IngredientName.Mistake)
        }, {
            RecipeName.ChickenNoodleSoup, new Recipe(
                RecipeName.ChickenNoodleSoup,
                TextureId.ChickenNoodleSoup,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Carrots, 1 },
                    { IngredientName.Chicken, 1 },
                    { IngredientName.Pasta, 1 },
                    { IngredientName.Potato, 1 }
                },
                IngredientName.ChickenNoodleSoup)
        }, {
            RecipeName.FrenchFries, new Recipe(
                RecipeName.FrenchFries,
                TextureId.FrenchFries,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Pepper, 1 },
                    { IngredientName.Potato, 1 },
                    { IngredientName.Salt, 1 }
                },
                IngredientName.FrenchFries)
        }, {
            RecipeName.FriedEgg, new Recipe(
                RecipeName.FriedEgg,
                TextureId.FriedEgg,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Egg, 1 },
                    { IngredientName.Pepper, 1 },
                    { IngredientName.Salt, 1 }
                },
                IngredientName.FriedEgg)
        }, {
            RecipeName.GrilledCheese, new Recipe(
                RecipeName.GrilledCheese,
                TextureId.GrilledCheese,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Bread, 1 },
                    { IngredientName.Butter, 1 },
                    { IngredientName.Cheese, 1 }
                },
                IngredientName.GrilledCheese)
        }, {
            RecipeName.Pancakes, new Recipe(
                RecipeName.Pancakes,
                TextureId.Pancakes,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Butter, 1 },
                    { IngredientName.Egg, 1 },
                    { IngredientName.Flour, 1 },
                    { IngredientName.Milk, 1 }
                },
                IngredientName.Pancakes)
        }, {
            RecipeName.Ramen, new Recipe(
                RecipeName.Ramen,
                TextureId.Ramen,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Carrots, 1 },
                    { IngredientName.Chicken, 1 },
                    { IngredientName.Egg, 1 },
                    { IngredientName.Noodle, 1 }
                },
                IngredientName.Ramen)
        }, {
            RecipeName.SteakFrite, new Recipe(
                RecipeName.SteakFrite,
                TextureId.SteakFrite,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Beef, 1 },
                    { IngredientName.Garlic, 1 },
                    { IngredientName.Onion, 1 },
                    { IngredientName.Pepper, 1 },
                    { IngredientName.Potato, 1 },
                    { IngredientName.Salt, 1 }
                },
                IngredientName.SteakFrite)
        }, {
            RecipeName.TomatoBisque, new Recipe(
                RecipeName.TomatoBisque,
                TextureId.TomatoBisque,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Carrots, 1 },
                    { IngredientName.Milk, 1 },
                    { IngredientName.Onion, 1 },
                    { IngredientName.Tomato, 1 }
                },
                IngredientName.TomatoBisque)
        },
        {
            RecipeName.Pizza, new Recipe(
                RecipeName.Pizza,
                TextureId.Pizza,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Bread, 1 },
                    { IngredientName.Butter, 1 },
                    { IngredientName.Cheese, 1 },
                    { IngredientName.Onion, 1 },
                    { IngredientName.Tomato, 1 }
                },
                IngredientName.Pizza)
        },
        {
            RecipeName.FriedChicken, new Recipe(
                RecipeName.FriedChicken,
                TextureId.FriedChicken,
                new Dictionary<IngredientName, int>() {
                    { IngredientName.Butter, 1 },
                    { IngredientName.Chicken, 1 },
                    { IngredientName.Flour, 1 },
                    { IngredientName.Pepper, 1 },
                    { IngredientName.Salt, 1 }
                },
                IngredientName.FriedChicken)
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

public record Recipe(RecipeName Name, TextureId TextureId, Dictionary<IngredientName, int> Ingredients, IngredientName IngredientName);

public enum RecipeName {
    Mistake = 0,
    ChickenNoodleSoup = 1,
    FrenchFries = 2,
    FriedEgg = 3,
    GrilledCheese = 4,
    Pancakes = 5,
    Ramen = 6,
    SteakFrite = 7,
    TomatoBisque = 8,

    FriedChicken = 9,
    Pizza = 10,
}