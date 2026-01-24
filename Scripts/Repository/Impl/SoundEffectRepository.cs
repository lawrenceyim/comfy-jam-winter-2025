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
    SuccessfulCooking = 2_001,
    UnsuccessfulCooking = 2_002,
    Beef = 3_001,
    Bread = 3_002,
    Butter = 3_003,
    Carrots = 3_004,
    Cheese = 3_005,
    Chicken = 3_006,
    Egg = 3_007,
    Flour = 3_008,
    Garlic = 3_009,
    Milk = 3_010,
    Onion = 3_011,
    Pasta = 3_012,
    Pepper = 3_013,
    Potato = 3_014,
    Salt = 3_015,
    Tomato = 3_016,
    FoxEat = 4_001,
    FoxTalk = 4_002,
}