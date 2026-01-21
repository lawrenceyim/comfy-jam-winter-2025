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

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _sceneManager = serviceLocator.GetService<SceneManager>();
        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);
        _playerDataService = serviceLocator.GetService<PlayerDataService>();

        _storeButton.Pressed += () => _sceneManager.ChangeScene(SceneId.Store);
        _kitchenButton.Pressed += () => _sceneManager.ChangeScene(SceneId.Kitchen);
        _dayLabel.Text = $"{_playerDataService.GetDay()}";
        _currencyLabel.Text = $"{_playerDataService.GetMoney()}";
        _playerDataService.MoneyUpdated += () => _currencyLabel.Text = $"{_playerDataService.GetMoney()}";
        _food.Texture = null;

        // SetAnimalAnimation(AnimalView.AnimalAnimation.Eat);
        // SetFood(RecipeName.ChickenNoodleSoup);
    }

    public void SetAnimalAnimation(AnimalView.AnimalAnimation animalAnimation) {
        switch (animalAnimation) {
            case AnimalView.AnimalAnimation.Eat:
                _animal.Position = _eatingAnimalPosition;
                break;
            case AnimalView.AnimalAnimation.Talk:
                _animal.DisplaySpeechBubble(true);
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

        TextureId recipeTextureId = recipeName switch {
            RecipeName.Mistake => TextureId.Mistake,
            RecipeName.ChickenNoodleSoup => TextureId.ChickenNoodleSoup,
            RecipeName.FrenchFries => TextureId.FrenchFries,
            RecipeName.FriedEgg => TextureId.FriedEgg,
            RecipeName.GrilledCheese => TextureId.GrilledCheese,
            RecipeName.Pancakes => TextureId.Pancakes,
            RecipeName.Ramen => TextureId.Ramen,
            RecipeName.SteakFrite => TextureId.SteakFrite,
            RecipeName.TomatoBisque => TextureId.TomatoBisque,
        };

        _food.Texture = _textureRepository.GetTexture(recipeTextureId);
    }
}