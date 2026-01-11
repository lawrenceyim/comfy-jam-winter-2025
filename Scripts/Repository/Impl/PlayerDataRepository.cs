using System.Collections.Generic;
using RepositorySystem;

public class PlayerDataRepository : IRepository {
    public Dictionary<IngredientName, int> Ingredients { get; } = new();
    public Dictionary<IngredientName, int> StoreInventory { get; } = new();
    public HashSet<RecipeName> DiscoveredRecipes { get; } = [];
    public HashSet<RecipeName> AnimalLikes { get; } = [];
    public HashSet<RecipeName> AnimalDislikes { get; } = [];
    public int AnimalHealth { get; set; } = 0;
    public int AnimalHealthRequired { get; } = 100;
    public RecipeName? CookedDish { get; set; } = null;
}