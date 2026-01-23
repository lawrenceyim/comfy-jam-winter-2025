using Godot;
using ServiceSystem;

public class SceneManager : IService {
    private SceneRepository _sceneRepository;
    private SceneId _currentSceneId;
    private SceneId _previousSceneId;
    private SceneId _nextSceneId;

    public SceneManager(SceneRepository sceneRepository) {
        _sceneRepository = sceneRepository;
    }

    public void SetCurrentSceneId(SceneId sceneId) {
        _previousSceneId = _currentSceneId;
        _currentSceneId = sceneId;
    }

    public void SetNextSceneId(SceneId sceneId) {
        _nextSceneId = sceneId;
    }

    public void ChangeToCurrentScene() {
        ChangeScene(_currentSceneId);
    }

    public void ChangeToNextScene() {
        ChangeScene(_nextSceneId);
    }

    public void ChangeToPreviousScene() {
        (_currentSceneId, _previousSceneId) = (_previousSceneId, _currentSceneId);
        ChangeScene(_currentSceneId);
    }

    public void ChangeScene(SceneId sceneId) {
        (Engine.GetMainLoop() as SceneTree)?.ChangeSceneToPacked(_sceneRepository.GetPackedScene(sceneId));
    }

    public SceneId GetCurrentSceneId() {
        return _currentSceneId;
    }

    public SceneId GetPreviousSceneId() {
        return _previousSceneId;
    }
}