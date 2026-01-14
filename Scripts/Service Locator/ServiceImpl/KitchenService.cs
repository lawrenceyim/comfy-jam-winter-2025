using System.Collections.Generic;
using ServiceSystem;

public class KitchenService : IService {
    private readonly InventoryService _inventoryService;

    public KitchenService(InventoryService inventoryService) {
        _inventoryService = inventoryService;
    }

    public void Cook(RecipeName RecipeName) {
        Recipe recipe = RecipeInfo.GetRecipe(RecipeName);
        foreach (KeyValuePair<IngredientName, int> ingredient in recipe.Ingredients) {
            _inventoryService.DecreaseIngredientQuantity(ingredient.Key, ingredient.Value);
        }

        _inventoryService.IncreaseIngredientQuantity(recipe.IngredientName, 1);
    }
}