using Godot;
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

    private PlayerDataService _playerDataService;
    private SceneManager _sceneManager;

    private Vector2 _regularAnimalPosition = new(610, 470);
    private Vector2 _eatingAnimalPosition = new(200, 300);

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _sceneManager = serviceLocator.GetService<SceneManager>();
        _playerDataService = serviceLocator.GetService<PlayerDataService>();

        _storeButton.Pressed += () => _sceneManager.ChangeScene(SceneId.Store);
        _kitchenButton.Pressed += () => _sceneManager.ChangeScene(SceneId.Kitchen);
        _dayLabel.Text = $"{_playerDataService.GetDay()}";
        _currencyLabel.Text = $"{_playerDataService.GetMoney()}";
        _playerDataService.MoneyUpdated += () => _currencyLabel.Text = $"{_playerDataService.GetMoney()}";
    }

    public void SetAnimalAnimation(AnimalView.AnimalAnimation animalAnimation) {
        switch (animalAnimation) {
            case AnimalView.AnimalAnimation.Eat:
                _animal.Position = _eatingAnimalPosition;
                break;
            default:
                _animal.Position = _regularAnimalPosition;
                break;
        }
        
        _animal.PlayAnimation(animalAnimation);
    }
}