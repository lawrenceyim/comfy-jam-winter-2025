using System.Collections.Generic;
using Godot;
using ServiceSystem;

public partial class InventoryView : Node2D {
	private InventoryService _inventoryService;

	public override void _Ready() {
		ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
		_inventoryService = serviceLocator.GetService<InventoryService>();
		_DisplayInventory();
	}

	private void _DisplayInventory() {
		List<IngredientDto> ingredients = _inventoryService.GetAllIngredients();
		// TODO: Replace with UI
		foreach (IngredientDto ingredient in ingredients) {
			GD.Print($"ingredient: {ingredient.Ingredient} {ingredient.Quantity}");
		}
	}
}
