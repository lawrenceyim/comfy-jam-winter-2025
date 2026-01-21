using System.Collections.Generic;
using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class CookbookView : Node2D {
	[Export]
	private Sprite2D _selectedRecipeSprite;

	[Export]
	private Label _selectedRecipeLabel;

	private TextureRepository _textureRepository;

	private static readonly Vector2 _ingredientCardScale = new Vector2(.065f, .065f);
	private Sprite2D[] _ingredientCards = new Sprite2D[6];
	private Vector2 _ingredientCardSize = new Vector2(1852, 2516) * _ingredientCardScale;
	private Vector2 _ingredientCardOffset = new(10, 25);
	private Vector2 _ingredientCardStartPosition = new(720, 250);

	public override void _Ready() {
		ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
		RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
		_textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);

		for (int i = 0; i < _ingredientCards.Length; i++) {
			Sprite2D sprite = new Sprite2D();
			AddChild(sprite);
			_ingredientCards[i] = sprite;

			int column = i % 3;
			int row = i / 3;

			float x = _ingredientCardStartPosition.X
					  + column * (_ingredientCardSize.X + _ingredientCardOffset.X);

			float y = _ingredientCardStartPosition.Y
					  + row * (_ingredientCardSize.Y + _ingredientCardOffset.Y);

			sprite.Position = new Vector2(x, y);
			sprite.Scale = _ingredientCardScale;
		}

		// TODO: Remove test
		SetSelectedRecipe(RecipeName.SteakFrite);
		DisplaySelectedRecipe(true);
	}

	public void DisplaySelectedRecipe(bool visible) {
		_selectedRecipeSprite.Visible = visible;
		foreach (Sprite2D sprite in _ingredientCards) {
			sprite.Visible = visible;
		}
	}

	public void ClearSelectedRecipe() {
		_selectedRecipeLabel.Text = string.Empty;
		_selectedRecipeSprite.Texture = null;
		foreach (Sprite2D sprite in _ingredientCards) {
			sprite.Texture = null;
		}
	}

	public void SetSelectedRecipe(RecipeName recipeName) {
		Recipe recipe = RecipeInfo.GetRecipe(recipeName);

		_selectedRecipeLabel.Text = StringUtils.SplitPascalCase(recipe.Name.ToString());

		TextureId textureId = recipeName switch {
			RecipeName.Mistake => TextureId.Mistake,
			RecipeName.ChickenNoodleSoup => TextureId.ChickenNoodleSoup,
			RecipeName.FrenchFries => TextureId.FrenchFries,
			RecipeName.FriedEgg => TextureId.FriedEgg,
			RecipeName.GrilledCheese => TextureId.GrilledCheese,
			RecipeName.Pancakes => TextureId.Pancakes,
			RecipeName.Ramen => TextureId.Ramen,
			RecipeName.SteakFrite => TextureId.SteakFrite,
			RecipeName.TomatoBisque => TextureId.TomatoBisque,
			_ => TextureId.Mistake
		};

		_selectedRecipeSprite.Texture = _textureRepository.GetTexture(textureId);

		int ingredientIndex = 0;
		foreach (KeyValuePair<IngredientName, int> kvp in recipe.Ingredients) {
			_ingredientCards[ingredientIndex++].Texture = _textureRepository.GetTexture(IngredientUtil.GetTextureId(kvp.Key));
		}
	}
}
