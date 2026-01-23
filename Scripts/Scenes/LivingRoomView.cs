using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class LivingRoomView : Node2D {
    [Export]
    private TextureButton _storeButton;

    [Export]
    private TextureButton _kitchenButton;

    [Export]
    private TextureButton _bookButton;

    [Export]
    private Label _dayLabel;

    [Export]
    private Label _currencyLabel;

    [Export]
    private AnimalView _animal;

    [Export]
    private Sprite2D _food;

    private PlayerDataService _playerDataService;
    private SceneManager _sceneManager;

    private Vector2 _regularAnimalPosition = new(610, 470);
    private Vector2 _eatingAnimalPosition = new(200, 300);
    private TextureRepository _textureRepository;
    private TickTimer _eatingAnimationTimer = new();

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _sceneManager = serviceLocator.GetService<SceneManager>();
        _sceneManager.SetCurrentSceneId(SceneId.LivingRoom);

        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);
        _playerDataService = serviceLocator.GetService<PlayerDataService>();

        _storeButton.Pressed += () => _sceneManager.ChangeScene(SceneId.Store);
        _kitchenButton.Pressed += () => {
            _sceneManager.SetNextSceneId(SceneId.Kitchen);
            _sceneManager.ChangeScene(TransitionUtil.GetRandomTransitionSceneId());
        };
        _bookButton.Pressed += () => _sceneManager.ChangeScene(SceneId.CookBoox);

        _dayLabel.Text = $"{_playerDataService.GetDay()}";
        _currencyLabel.Text = $"{_playerDataService.GetMoney()}";
        _playerDataService.MoneyUpdated += () => _currencyLabel.Text = $"{_playerDataService.GetMoney()}";
        _food.Texture = null;

        _eatingAnimationTimer.TimedOut += () => {
            _playerDataService.SetRecipeMade(null);
            SetFood(null);
            SetAnimalAnimation(AnimalView.AnimalAnimation.Talk);

            if (_playerDataService.DiscoveredAllRecipes()) {
                // TODO: Add transition to end scene
                
                GD.Print("Discovered all recipes");
            }
        };

        if (_playerDataService.GetRecipeMade() is not null) {
            SetAnimalAnimation(AnimalView.AnimalAnimation.Eat);
            SetFood(_playerDataService.GetRecipeMade());
            _eatingAnimationTimer.StartFixedTimer(false, 5 * Engine.PhysicsTicksPerSecond);
        }
    }

    public override void _PhysicsProcess(double delta) {
        _eatingAnimationTimer.PhysicsTick();
    }

    public void SetAnimalAnimation(AnimalView.AnimalAnimation animalAnimation) {
        switch (animalAnimation) {
            case AnimalView.AnimalAnimation.Eat:
                _animal.Position = _eatingAnimalPosition;
                break;
            case AnimalView.AnimalAnimation.Talk:
                _animal.DisplaySpeechBubble(true);
                _animal.Position = _regularAnimalPosition;
                break;
            default:
                _animal.Position = _regularAnimalPosition;
                break;
        }

        _animal.PlayAnimation(animalAnimation);
    }

    public void DisplaySpeech(bool animalSpeech, string text) {
        if (animalSpeech) {
            SetAnimalAnimation(AnimalView.AnimalAnimation.Talk);
            _animal.SetSpeechLabel(text);
            // hide player text
        } else {
            // show player text
            SetAnimalAnimation(AnimalView.AnimalAnimation.Idle);
            _animal.DisplaySpeechBubble(false);
        }
    }

    public void SetFood(RecipeName? recipeName) {
        if (recipeName == null) {
            _food.Texture = null;
            return;
        }

        TextureId recipeTextureId = RecipeUtil.GetTextureId(recipeName.Value);
        _food.Texture = _textureRepository.GetTexture(recipeTextureId);
    }
}