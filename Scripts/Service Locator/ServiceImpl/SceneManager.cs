using Godot;
using ServiceSystem;

public class SceneManager : IService {
    private SceneRepository _sceneRepository;
    private SceneId _currentSceneId;

    public SceneManager(SceneRepository sceneRepository) {
        _sceneRepository = sceneRepository;
    }

    public void SetCurrentSceneId(SceneId sceneId) {
        _currentSceneId = sceneId;
    }

    public void ChangeToCurrentScene() {
        ChangeScene(_currentSceneId);
    }

    public void ChangeScene(SceneId sceneId) {
        (Engine.GetMainLoop() as SceneTree)?.ChangeSceneToPacked(_sceneRepository.GetPackedScene(sceneId));
    }
}