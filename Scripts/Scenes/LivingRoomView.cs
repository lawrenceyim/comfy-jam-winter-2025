using Godot;
using System;
using ServiceSystem;

public partial class LivingRoomView : Node2D {
    [Export]
    private Button _storeButton;

    [Export]
    private Button _kitchenButton;

    [Export]
    private Label _dayLabel;

    [Export]
    private HSlider _healthSlider;

    private SceneManager _sceneManager;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _sceneManager = serviceLocator.GetService<SceneManager>();

        _storeButton.Pressed += () => _sceneManager.ChangeScene(SceneId.Store);
        _kitchenButton.Pressed += () => _sceneManager.ChangeScene(SceneId.Kitchen);
    }
}