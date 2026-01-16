using System.Collections.Generic;
using RepositorySystem;

public class PlayerDataRepository : IRepository {
    public Dictionary<IngredientName, int> Ingredients { get; } = new();
    public Dictionary<IngredientName, int> StoreInventory { get; } = new();
    public HashSet<RecipeName> DiscoveredRecipes { get; } = [];
    public HashSet<RecipeName> AnimalLikes { get; } = [];
    public HashSet<RecipeName> AnimalDislikes { get; } = [];
    public RecipeName? CookedDish { get; set; } = null;
    public int Money { get; set; } = 0;
    public int Day { get; set; } = 1;
}