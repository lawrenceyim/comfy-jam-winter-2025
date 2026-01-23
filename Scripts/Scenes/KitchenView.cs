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

    [Export]
    private Node2D _cookingUI;

    [Export]
    private AnimatedSprite2D _fryingPan;

    private readonly TickTimer _cookingAnimationTimer = new();

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
        _cardDisplay.RecipeMadeVsIntended += _HandleRecipeMade;

        _DisplayRecipeSprites(false);
        _kitchenService.SelectRandomRecipeIfNull();

        _cookingAnimationTimer.TimedOut += _FinishCookingAnimation;

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

    public override void _Process(double delta) {
        _cookingAnimationTimer.PhysicsTick();
    }

    private void _DisplayRecipeSprites(bool display) {
        _cookedRecipeSprite.Visible = display;
        _cookedBox.Visible = display;
    }

    private void _HandleRecipeMade(RecipeName made, RecipeName intended) {
        _playerDataService.SetRecipeMade(made);
        _cookedRecipeSprite.Texture = _textureRepository.GetTexture(RecipeUtil.GetTextureId(made));
        _DisplayCookingUi(true);
        _SetCookingAnimation(intended);
        _cookingAnimationTimer.StartFixedTimer(false, 3 * Engine.PhysicsTicksPerSecond);
    }

    private void _FinishCookingAnimation() {
        _DisplayButtons(true);
        _DisplayRecipeSprites(true);
        _DisplayCookingUi(false);
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

    private void _DisplayCookingUi(bool display) {
        _cookingUI.Visible = display;
    }

    private void _SetCookingAnimation(RecipeName recipe) {
        _fryingPan.Play(recipe switch {
            RecipeName.ChickenNoodleSoup => "Chicken Noodle Soup",
            RecipeName.FrenchFries => "Fries",
            RecipeName.FriedChicken => "Fried Chicken",
            RecipeName.FriedEgg => "Fried Egg",
            RecipeName.GrilledCheese => "Grilled Cheese",
            RecipeName.Pancakes => "Pancake",
            RecipeName.Pizza => "Pizza",
            RecipeName.SteakFrite => "Steak",
            RecipeName.TomatoBisque => "Tomato",
        });
    }
}