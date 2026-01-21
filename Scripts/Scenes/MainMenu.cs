using Godot;
using ServiceSystem;

public partial class MainMenu : Node2D {
    [Export]
    private Button _startButton;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        SceneManager sceneManager = serviceLocator.GetService<SceneManager>();

        _startButton.Pressed += () => sceneManager.ChangeScene(SceneId.LivingRoom);
    }
}