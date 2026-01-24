using Godot;
using Godot.Collections;
using RepositorySystem;

public partial class SoundEffectRepository : Node, IAutoload, IRepository {
    public static string AutoloadPath => "/root/SoundEffectRepository";

    [Export]
    private Dictionary<SoundEffectId, AudioStream> _soundEffects;

    public AudioStream GetSoundEffect(SoundEffectId id) {
        return _soundEffects[id];
    }
}

public enum SoundEffectId {
    ChickenNoodleSoup = 1_001,
    FrenchFries = 1_002,
    FriedChicken = 1_003,
    FriedEgg = 1_004,
    GrilledCheese = 1_005,
    Pancakes = 1_006,
    Pizza = 1_007,
    SteakFrite = 1_008,
    TomatoBisque = 1_009,
}