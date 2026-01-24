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
	End = 1,
	LivingRoom = 1_001,
	Kitchen = 1_002,
	Store = 1_003,
	CookBoox = 1_004,
	Inventory = 1_005,
	TransitionZoneOne = 2_001,
	TransitionZoneTwo = 2_002,
	TransitionZoneThree = 2_003,
}
