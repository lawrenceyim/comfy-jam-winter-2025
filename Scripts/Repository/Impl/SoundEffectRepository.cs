using Godot;
using Godot.Collections;
using RepositorySystem;

public partial class SoundEffectRepository : Node, IAutoload, IRepository {
	public static string AutoloadPath { get; } = "/root/SoundEffectRepository";

	[Export]
	private Dictionary<SoundEffectId, AudioStream> _soundEffects;

	public AudioStream GetSoundEffect(SoundEffectId id) {
		return _soundEffects[id];
	}
}

public enum SoundEffectId {

}
