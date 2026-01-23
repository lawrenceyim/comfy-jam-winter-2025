using System;
using System.Collections.Generic;
using ServiceSystem;

public class KitchenService : IService {
    private readonly InventoryService _inventoryService;
    private readonly PlayerDataRepository _playerDataRepository;
    private readonly Random _random = new();

    public KitchenService(InventoryService inventoryService, PlayerDataRepository playerDataRepository) {
        _inventoryService = inventoryService;
        _playerDataRepository = playerDataRepository;
    }

    public void SelectRandomRecipeIfNull() {
        if (_playerDataRepository.SelectedRecipe == null) {
            _playerDataRepository.SelectedRecipe = GetRandomRecipeName();
            GenerateRandomIngredientsForSelectedRecipe(_playerDataRepository.SelectedRecipe.Value);
        }
    }

    public void RandomizeProvidedIngredients() {
        GenerateRandomIngredientsForSelectedRecipe(_playerDataRepository.SelectedRecipe.Value);
    }

    public RecipeName GetRandomRecipeName() {
        const int start = (int)RecipeName.ChickenNoodleSoup;
        const int end = (int)RecipeName.Pizza + 1;
        HashSet<int> removedRecipes = [6]; // Ramen is gone
        int index;

        foreach (RecipeName recipe in _playerDataRepository.DiscoveredRecipes) {
            removedRecipes.Add((int)recipe);
        }

        do {
            index = _random.Next(start, end);
        } while (removedRecipes.Contains(index));

        return (RecipeName)index;
    }

    public HashSet<IngredientName> GetProvidedIngredients() {
        return _playerDataRepository.ProvidedIngredients;
    }

    public HashSet<IngredientName> GenerateRandomIngredientsForSelectedRecipe(RecipeName recipeName) {
        HashSet<IngredientName> providedIngredients = _playerDataRepository.ProvidedIngredients;
        providedIngredients.Clear();
        Recipe recipe = RecipeInfo.GetRecipe(recipeName);
        foreach (KeyValuePair<IngredientName, int> kvp in recipe.Ingredients) {
            providedIngredients.Add(kvp.Key);
        }

        IngredientName randomIngredient;
        do {
            randomIngredient = IngredientUtil.GetRandomIngredientName();
        } while (providedIngredients.Contains(randomIngredient));

        providedIngredients.Add(randomIngredient);

        return providedIngredients;
    }

    public void RecipeDiscovered(RecipeName recipeName) {
        _playerDataRepository.DiscoveredRecipes.Add(recipeName);
    }

    public RecipeName GetSelectedRecipe() {
        return _playerDataRepository.SelectedRecipe.Value;
    }

    public void ClearSelectedRecipe() {
        _playerDataRepository.SelectedRecipe = null;
    }
}