using ServiceSystem;

public class StoreService : IService {
    private readonly InventoryService _inventoryService;
    private readonly PlayerDataService _playerDataService;

    public StoreService(PlayerDataService playerDataService, InventoryService inventoryService) {
        _playerDataService = playerDataService;
        _inventoryService = inventoryService;
    }

    public void PurchaseIngredient(IngredientName ingredient, int quantity) {
        _inventoryService.IncreaseIngredientQuantity(ingredient, quantity);
        _playerDataService.SetMoney(_playerDataService.GetMoney() - quantity); // TODO: change to calculate actual money
    }
}