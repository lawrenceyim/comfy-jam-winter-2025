using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class KitchenView : Node2D {
    private TextureRepository _textureRepository;
    private PlayerDataService _playerDataService;
    private SceneManager _sceneManager;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _playerDataService = serviceLocator.GetService<PlayerDataService>();

        _sceneManager = serviceLocator.GetService<SceneManager>();
        _sceneManager.SetCurrentSceneId(SceneId.Kitchen);

        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);
    }
}