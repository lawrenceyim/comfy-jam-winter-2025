using System.Collections.Generic;
using Godot;
using ServiceSystem;

namespace RepositorySystem;

public partial class RepositoryLocator : Node, IService {
	private Dictionary<RepositoryName, IRepository> _repositories = new() {
	};

	public override void _EnterTree() {
		AddRepository(RepositoryName.PackedScene, GetNode<PackedSceneRepository>(PackedSceneRepository.AutoloadPath));
		AddRepository(RepositoryName.Scene, GetNode<SceneRepository>(SceneRepository.AutoloadPath));
		AddRepository(RepositoryName.Texture, GetNode<Texture2dRepository>(Texture2dRepository.AutoloadPath));
		AddRepository(RepositoryName.SoundEffect, GetNode<SoundEffectRepository>(SoundEffectRepository.AutoloadPath));
		AddRepository(RepositoryName.PlayerData, new PlayerDataRepository());
	}

	public void AddRepository(RepositoryName repositoryName, IRepository repository) {
		_repositories.Add(repositoryName, repository);
	}

	public T GetRepository<T>(RepositoryName repositoryName) {
		return (T)_repositories[repositoryName];
	}
}
