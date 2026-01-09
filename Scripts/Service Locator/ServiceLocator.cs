using System.Collections.Generic;
using Godot;
using InputSystem;
using RepositorySystem;

namespace ServiceSystem;

public partial class ServiceLocator : Node, IAutoload {
    public static string AutoloadPath => "/root/ServiceLocator";
    private readonly Dictionary<ServiceName, object> _services = new Dictionary<ServiceName, object>();

    public override void _EnterTree() {
        _InstantiateServices();
    }

    public void AddService(ServiceName serviceName, IService service, bool addChild) {
        _services[serviceName] = service;
        if (addChild) {
            AddChild(service as Node);
        }
    }

    public void RemoveService(ServiceName serviceName) {
        _services.Remove(serviceName);
    }

    public T GetService<T>(ServiceName serviceName) {
        return (T)_services[serviceName];
    }

    private void _InstantiateServices() {
        RepositoryLocator repositoryLocator = new();
        AddService(ServiceName.RepositoryLocator, repositoryLocator, true);

        PlayerDataRepository playerDataRepository = repositoryLocator.GetRepository<PlayerDataRepository>(RepositoryName.PlayerData);
        SceneRepository sceneRepository = repositoryLocator.GetRepository<SceneRepository>(RepositoryName.Scene);
        AddService(ServiceName.GameClock, new GameClock(), true);
        AddService(ServiceName.InputStateMachine, new InputStateMachine(), true);
        AddService(ServiceName.PlayerData, new PlayerDataService(playerDataRepository), false);
        AddService(ServiceName.SceneManager, new SceneManager(sceneRepository), false);
    }
}