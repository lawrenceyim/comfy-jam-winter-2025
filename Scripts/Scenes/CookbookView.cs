using System.Collections.Generic;
using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class CookbookView : Node2D {
	[Export]
	private Sprite2D _selectedRecipeSprite;

	[Export]
	private Label _selectedRecipeLabel;

	[Export]
	private Button _exitButton;

	[Export]
	private Button _nextPageButton;

	[Export]
	private Button _lastPageButton;

	[Export]
	private Node2D _kitchenBackground;

	[Export]
	private Node2D _livingRoomBackground;

	private TextureRepository _textureRepository;
	private PlayerDataService _playerDataService;
	private SceneManager _sceneManager;

	private static readonly Vector2 _ingredientCardScale = new Vector2(.065f, .065f);
	private Sprite2D[] _ingredientCards = new Sprite2D[6];
	private Vector2 _ingredientCardSize = new Vector2(1852, 2516) * _ingredientCardScale;
	private Vector2 _ingredientCardOffset = new(10, 25);
	private Vector2 _ingredientCardStartPosition = new(720, 250);

	private const int LastPage = 10;
	private int _currentPage = 0;

	public override void _Ready() {
		ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
		_playerDataService = serviceLocator.GetService<PlayerDataService>();

		_sceneManager = serviceLocator.GetService<SceneManager>();
		_sceneManager.SetCurrentSceneId(SceneId.CookBoox);

		RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
		_textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);

		_nextPageButton.Pressed += () => _NavigatePage(true);
		_lastPageButton.Pressed += () => _NavigatePage(false);
		_exitButton.Pressed += () => _sceneManager.ChangeToPreviousScene();

		_SetBackground();
		_InitIngredientCards();
		_DisplayPageNavButtons();
		SetSelectedRecipe(RecipeUtil.GetRecipeNameSorted(_currentPage));
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
		bool discovered = _playerDataService.IsRecipeDiscovered(recipeName);

		_selectedRecipeLabel.Text = discovered
			? $"{_currentPage}. {StringUtils.SplitPascalCase(recipe.Name.ToString())}"
			: $"{_currentPage}. ?";

		TextureId textureId = discovered
			? RecipeUtil.GetTextureId(recipeName)
			: TextureId.UnknownRecipe;

		_selectedRecipeSprite.Texture = _textureRepository.GetTexture(textureId);

		int ingredientIndex = 0;
		foreach (KeyValuePair<IngredientName, int> kvp in recipe.Ingredients) {
			_ingredientCards[ingredientIndex++].Texture = discovered
				? _textureRepository.GetTexture(IngredientUtil.GetTextureId(kvp.Key))
				: _textureRepository.GetTexture(TextureId.UnknownIngredient);
		}
	}

	private void _DisplayPageNavButtons() {
		if (_currentPage == 0) {
			_nextPageButton.Visible = true;
			_lastPageButton.Visible = false;
			return;
		}

		if (_currentPage == LastPage) {
			_nextPageButton.Visible = false;
			_lastPageButton.Visible = true;
			return;
		}

		_nextPageButton.Visible = true;
		_lastPageButton.Visible = true;
	}

	private void _NavigatePage(bool next) {
		_currentPage += next ? 1 : -1;
		_DisplayPageNavButtons();
		ClearSelectedRecipe();
		SetSelectedRecipe(RecipeUtil.GetRecipeNameSorted(_currentPage));
	}

	private void _InitIngredientCards() {
		for (int i = 0; i < _ingredientCards.Length; i++) {
			Sprite2D sprite = new();
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
	}

	private void _SetBackground() {
		SceneId previousScene = _sceneManager.GetPreviousSceneId();
		switch (previousScene) {
			case SceneId.LivingRoom:
				_livingRoomBackground.Visible = true;
				break;
			case SceneId.Kitchen:
				_kitchenBackground.Visible = true;
				break;
		}
	}
}
