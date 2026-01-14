using System;
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

    public T GetService<T>() where T : IService {
        return (T)_services[_ConvertToEnum(typeof(T))];
    }

    private static ServiceName _ConvertToEnum(Type objectType) {
        GD.Print($"Received IService of {objectType} for conversion to enum");
        return objectType switch {
            not null when objectType == typeof(GameClock) => ServiceName.GameClock,
            not null when objectType == typeof(InputStateMachine) => ServiceName.InputStateMachine,
            not null when objectType == typeof(PlayerDataRepository) => ServiceName.PlayerData,
            not null when objectType == typeof(RepositoryLocator) => ServiceName.RepositoryLocator,
            not null when objectType == typeof(SceneManager) => ServiceName.SceneManager,
            not null when objectType == typeof(InventoryService) => ServiceName.InventoryService,
        };
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

        InventoryService inventoryService = new(playerDataRepository);
        AddService(ServiceName.InventoryService, inventoryService, false);
        AddService(ServiceName.KitchenService, new KitchenService(inventoryService), false);
    }
}