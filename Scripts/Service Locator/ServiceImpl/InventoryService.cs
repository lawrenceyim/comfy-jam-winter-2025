using System.Collections.Generic;
using System.Linq;
using ServiceSystem;

public class InventoryService : IService {
    private readonly PlayerDataRepository _playerDataRepository;

    public InventoryService(PlayerDataRepository playerDataRepository) {
        _playerDataRepository = playerDataRepository;
    }

    public bool HasSufficientIngredients(IngredientName ingredient, int quantity) {
        return _playerDataRepository.Ingredients.ContainsKey(ingredient) &&
               _playerDataRepository.Ingredients[ingredient] >= quantity;
    }

    public void SetIngredientQuantity(IngredientName ingredient, int quantity) {
        _playerDataRepository.Ingredients[ingredient] = quantity;
    }

    public void IncreaseIngredientQuantity(IngredientName ingredient, int quantity) {
        _playerDataRepository.Ingredients[ingredient] = quantity + _playerDataRepository.Ingredients.GetValueOrDefault(ingredient, 0);
    }

    public void DecreaseIngredientQuantity(IngredientName ingredient, int quantity) {
        _playerDataRepository.Ingredients[ingredient] = _playerDataRepository.Ingredients.GetValueOrDefault(ingredient, 0) - quantity;
    }

    public int GetIngredientQuantity(IngredientName ingredient) {
        return _playerDataRepository.Ingredients.GetValueOrDefault(ingredient, 0);
    }

    public List<IngredientDto> GetAllIngredients() {
        List<IngredientDto> ingredients = [];

        // For Testing
        // IncreaseIngredientQuantity(IngredientName.Egg, 5);
        // IncreaseIngredientQuantity(IngredientName.Milk, 1);
        // IncreaseIngredientQuantity(IngredientName.Flour, 3);
        // IncreaseIngredientQuantity(IngredientName.Butter, 7);
        //

        foreach (KeyValuePair<IngredientName, int> ingredient in _playerDataRepository.Ingredients) {
            ingredients.Add(new IngredientDto(ingredient.Key, ingredient.Value));
        }

        // Should this be done on the view side to allow sorting by different value?
        // Example: alphabetically, quantity
        ingredients = ingredients.OrderBy(i => i.Ingredient.ToString()).ToList();
        return ingredients;
    }
}