using System.Collections.Generic;
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

    public int GetIngredientQuantity(IngredientName ingredient) {
        return _playerDataRepository.Ingredients.GetValueOrDefault(ingredient, 0);
    }
}