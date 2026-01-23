using System.Linq;
using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class KitchenView : Node2D {
    [Export]
    private TextureButton _cookBookButton;

    [Export]
    private Button _homeButton;

    [Export]
    private Button _playerResponseButton;

    [Export]
    private Label _playerResponseButtonLabel;

    [Export]
    private Node2D _playerResponseButtonUi;

    [Export]
    private CardDisplay _cardDisplay;

    [Export]
    private Sprite2D _cookedBox;

    [Export]
    private Sprite2D _cookedRecipeSprite;

    private TextureRepository _textureRepository;
    private PlayerDataService _playerDataService;
    private KitchenService _kitchenService;
    private SceneManager _sceneManager;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _playerDataService = serviceLocator.GetService<PlayerDataService>();

        _sceneManager = serviceLocator.GetService<SceneManager>();
        _sceneManager.SetCurrentSceneId(SceneId.Kitchen);

        _kitchenService = serviceLocator.GetService<KitchenService>();

        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);

        _cookBookButton.Pressed += () => _sceneManager.ChangeScene(SceneId.CookBoox);
        _cardDisplay.RecipeMade += _RecipeMade;

        _DisplayRecipeSprites(false);
        _kitchenService.SelectRandomRecipeIfNull();

        _homeButton.Pressed += () => {
            _sceneManager.SetNextSceneId(SceneId.LivingRoom);
            _sceneManager.ChangeScene(TransitionUtil.GetRandomTransitionSceneId());
        };

        _playerResponseButton.Pressed += () => {
            if (_playerDataService.GetRecipeMade() == RecipeName.Mistake) {
                _DisplayRecipeSprites(false);
                _kitchenService.RandomizeProvidedIngredients();
                _InitIngredients();
                _DisplayButtons(false);
            } else {
                _sceneManager.SetNextSceneId(SceneId.LivingRoom);
                _sceneManager.ChangeScene(TransitionUtil.GetRandomTransitionSceneId());
            }
        };

        _DisplayButtons(false);
        _InitIngredients();
    }

    private void _DisplayRecipeSprites(bool display) {
        _cookedRecipeSprite.Visible = display;
        _cookedBox.Visible = display;
    }

    private void _RecipeMade(RecipeName recipe) {
        _playerDataService.SetRecipeMade(recipe);
        _cookedRecipeSprite.Texture = _textureRepository.GetTexture(RecipeUtil.GetTextureId(recipe));
        _DisplayButtons(true);
        _DisplayRecipeSprites(true);
    }

    private void _InitIngredients() {
        _cardDisplay.InitIngredients(
            _kitchenService.GetProvidedIngredients()
                .OrderBy(i => i.ToString())
                .ToArray()
        );
    }

    private void _DisplayButtons(bool display) {
        _playerResponseButtonUi.Visible = display;

        if (display) {
            _playerResponseButtonLabel.Text =
                _playerDataService.GetRecipeMade() == RecipeName.Mistake
                    ? "Retry"
                    : "Eat";
        }
    }
}