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

    [Export]
    private AudioStreamPlayer _cookingSfx;

    [Export]
    private Label _recipeLabel;

    private readonly TickTimer _cookingAnimationTimer = new();

    private TextureRepository _textureRepository;
    private PlayerDataService _playerDataService;
    private KitchenService _kitchenService;
    private SceneManager _sceneManager;
    private SoundEffectRepository _soundEffectRepository;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _playerDataService = serviceLocator.GetService<PlayerDataService>();

        _sceneManager = serviceLocator.GetService<SceneManager>();
        _sceneManager.SetCurrentSceneId(SceneId.Kitchen);

        _kitchenService = serviceLocator.GetService<KitchenService>();

        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);
        _soundEffectRepository = repositoryLocator.GetRepository<SoundEffectRepository>(RepositoryName.SoundEffect);

        _cookBookButton.Pressed += () => _sceneManager.ChangeScene(SceneId.CookBoox);
        _cardDisplay.RecipeMadeVsIntended += _HandleRecipeMade;

        _DisplayRecipeSprites(false);
        _kitchenService.SelectRandomRecipeIfNull();

        _cookingAnimationTimer.TimedOut += _FinishCookingAnimation;

        _homeButton.Pressed += () => {
            _sceneManager.SetNextSceneId(SceneId.LivingRoom);
            _sceneManager.ChangeScene(TransitionUtil.GetRandomTransitionSceneId());
        };

        _playerResponseButton.Pressed += _HandlePlayerResponseButtonPressed;

        _DisplayButtons(false);
        _InitIngredients();
    }

    public override void _Process(double delta) {
        _cookingAnimationTimer.PhysicsTick();
    }

    private void _DisplayRecipeSprites(bool display) {
        _cookedRecipeSprite.Visible = display;
        _cookedBox.Visible = display;
        _recipeLabel.Visible = display;
    }

    private void _HandlePlayerResponseButtonPressed() {
        _cookingSfx.Stop();
        if (_playerDataService.GetRecipeMade() == RecipeName.Mistake) {
            _DisplayRecipeSprites(false);
            _kitchenService.RandomizeProvidedIngredients();
            _InitIngredients();
            _DisplayButtons(false);
        } else {
            _kitchenService.ClearSelectedRecipe();
            _sceneManager.SetNextSceneId(SceneId.LivingRoom);
            _sceneManager.ChangeScene(TransitionUtil.GetRandomTransitionSceneId());
        }
    }

    private void _HandleRecipeMade(RecipeName made, RecipeName intended) {
        _playerDataService.SetRecipeMade(made);
        _cookedRecipeSprite.Texture = _textureRepository.GetTexture(RecipeUtil.GetTextureId(made));
        _recipeLabel.Text = StringUtils.SplitPascalCase(made.ToString());
        _DisplayCookingUi(true);
        _SetCookingAnimation(intended);
        _cookingSfx.Stream = _soundEffectRepository.GetSoundEffect(
            intended switch {
                RecipeName.ChickenNoodleSoup => SoundEffectId.ChickenNoodleSoup,
                RecipeName.FrenchFries => SoundEffectId.FrenchFries,
                RecipeName.FriedChicken => SoundEffectId.FriedChicken,
                RecipeName.FriedEgg => SoundEffectId.FriedEgg,
                RecipeName.GrilledCheese => SoundEffectId.GrilledCheese,
                RecipeName.Pancakes => SoundEffectId.Pancakes,
                RecipeName.Pizza => SoundEffectId.Pizza,
                RecipeName.SteakFrite => SoundEffectId.SteakFrite,
                RecipeName.TomatoBisque => SoundEffectId.TomatoBisque,
            }
        );
        _cookingSfx.Play();

        _cookingAnimationTimer.StartFixedTimer(false, 3 * Engine.PhysicsTicksPerSecond);
    }

    private void _FinishCookingAnimation() {
        _cookingSfx.Stop();
        _cookingSfx.Stream = _soundEffectRepository.GetSoundEffect(
            _playerDataService.GetRecipeMade() != RecipeName.Mistake
                ? SoundEffectId.SuccessfulCooking
                : SoundEffectId.UnsuccessfulCooking);
        _cookingSfx.Play();
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