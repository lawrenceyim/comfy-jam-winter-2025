using Godot;
using Godot.Collections;
using RepositorySystem;

public partial class SceneRepository : Node, IAutoload, IRepository {
	public static string AutoloadPath { get; } = "/root/SceneRepository";

	[Export]
	private Dictionary<SceneId, PackedScene> _packedScenes;

	public PackedScene GetPackedScene(SceneId sceneId) {
		return _packedScenes[sceneId];
	}
}

public enum SceneId {
	MainMenu = 0,
	LevelOne = 1001,
}
