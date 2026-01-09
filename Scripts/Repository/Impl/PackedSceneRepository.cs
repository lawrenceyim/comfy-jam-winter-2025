using Godot;
using Godot.Collections;
using RepositorySystem;

public partial class PackedSceneRepository : Node, IAutoload, IRepository {
	public static string AutoloadPath => "/root/PackedSceneRepository";

	[Export]
	private Dictionary<PackedSceneId, PackedScene> _packedScenes;

	public PackedScene GetPackedScene(PackedSceneId packedSceneId) {
		return _packedScenes[packedSceneId];
	}
}

public enum PackedSceneId {
}
