using System.Linq;
using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class KitchenView : Node2D {
    [Export]
    private TextureButton _cookBookButton;

    [Export]
    private CardDisplay _cardDisplay;

    private TextureRepository _textureRepository;
    private PlayerDataService _playerDataService;
    private KitchenService _kitchenService;
    private SceneManager _sceneManager;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _playerDataService = serviceLocator.GetService<PlayerDataService>();

        _sceneManager = serviceLocator.GetService<SceneManager>();
        _sceneManager.SetCurrentSceneId(SceneId.Kitchen);

        _kitchenService = serviceLocator.GetService<KitchenService>();

        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);

        _cookBookButton.Pressed += () => _sceneManager.ChangeScene(SceneId.CookBoox);

        _kitchenService.SelectRandomRecipeIfNull();

        _cardDisplay.InitIngredients(
            _kitchenService.GetProvidedIngredients()
                .OrderBy(i => i.ToString())
                .ToArray()
        );
    }
}