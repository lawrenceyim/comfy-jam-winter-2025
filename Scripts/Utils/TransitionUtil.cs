using System;

public class TransitionUtil {
    private static readonly SceneId[] _transitionScenes = [
        SceneId.TransitionZoneOne,
        SceneId.TransitionZoneTwo,
        SceneId.TransitionZoneThree
    ];

    private static readonly Random _random = new();

    public static SceneId GetRandomTransitionSceneId() {
        return _transitionScenes[_random.Next(_transitionScenes.Length)];
    }
}