using Godot;

public partial class AnimalView : AnimatedSprite2D {
	public enum AnimalAnimation {
		Idle,
		Walk,
		Talk,
	}

	public override void _Ready() {
		PlayAnimation(AnimalAnimation.Idle);
	}

	public void PlayAnimation(AnimalAnimation animation) {
		string animationName = animation switch {
			AnimalAnimation.Idle => "Idle",
			AnimalAnimation.Walk => "Walk",
			AnimalAnimation.Talk => "Talk",
		};

		Play(animationName);
	}
}
