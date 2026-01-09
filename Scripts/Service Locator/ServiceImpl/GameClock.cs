using System.Collections.Generic;
using Godot;
using ServiceSystem;

public partial class GameClock : Node, IService {
    private readonly Dictionary<ulong, ITick> _activeScenes = new();
    private bool _paused = false;

    public void AddActiveScene(ITick scene, ulong id) {
        _activeScenes.Add(id, scene);
    }

    public void RemoveActiveScene(ulong id) {
        _activeScenes.Remove(id);
    }

    public void RemoveAllActiveScenes() {
        _activeScenes.Clear();
    }

    public void SetPauseState(bool paused) {
        _paused = paused;
    }

    public bool IsPaused() {
        return _paused;
    }

    public override void _PhysicsProcess(double delta) {
        if (_paused) {
            return;
        }

        foreach (ITick scene in _activeScenes.Values) {
            scene?.PhysicsTick();
        }
    }
}