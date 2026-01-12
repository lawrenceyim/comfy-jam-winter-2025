using ServiceSystem;

public class StoreService : IService {
    private readonly PlayerDataRepository _playerDataRepository;
    private readonly InventoryService _inventoryService;

    public StoreService(PlayerDataRepository playerDataRepository, InventoryService inventoryService) {
        _playerDataRepository = playerDataRepository;
        _inventoryService = inventoryService;
    }

    public void PurchaseIngredient(IngredientName ingredient, int quantity) {
        _inventoryService.IncreaseIngredientQuantity(ingredient, quantity);
        _playerDataRepository.Money -= quantity; // TODO: quantity * COST <- figure out how to compute this
    }
}