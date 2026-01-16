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

	private PlayerDataService _playerDataService;
	private SceneManager _sceneManager;

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
}
