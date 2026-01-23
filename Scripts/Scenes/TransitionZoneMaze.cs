using Godot;
using System;
using ServiceSystem;

public partial class TransitionZoneMaze : Node2D {
    [Export]
    private Area2D _exitArea;

    public override void _Ready() {
        _exitArea.BodyEntered += (body) => {
            if (body is AnimalPlayable) {
                CallDeferred(nameof(_ExitTransitionZone));
            }
        };
    }

    private void _ExitTransitionZone() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        SceneManager sceneManager = serviceLocator.GetService<SceneManager>();
        sceneManager.ChangeToNextScene();
    }
}