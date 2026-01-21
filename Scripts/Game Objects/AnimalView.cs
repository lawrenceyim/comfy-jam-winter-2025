using Godot;

public partial class AnimalView : AnimatedSprite2D {
    public enum AnimalAnimation {
        Eat,
        Idle,
        Walk,
        Talk,
    }

    [Export]
    private AnimatedSprite2D _speechBubble;

    [Export]
    private Label _speechLabel;

    [Export]
    private AudioStreamPlayer _dialogueAudioPlayer;

    public override void _Ready() {
        PlayAnimation(AnimalAnimation.Idle);
        DisplaySpeechBubble(false);
    }

    public void DisplaySpeechBubble(bool displayed) {
        _speechBubble.Visible = displayed;
        if (displayed) {
            _speechBubble.Play();
            _dialogueAudioPlayer.Play();
        } else {
            _dialogueAudioPlayer.Stop();
        }
    }

    public void SetSpeechLabel(string text) {
        _speechLabel.Text = text;
    }

    public void PlayAnimation(AnimalAnimation animation) {
        string animationName = animation switch {
            AnimalAnimation.Idle => "Idle",
            AnimalAnimation.Walk => "Walk",
            AnimalAnimation.Talk => "Talk",
            AnimalAnimation.Eat => "Eat",
        };

        Play(animationName);
    }

    private void _PlayDialogueAudio(bool play) {
        if (play) {
            _dialogueAudioPlayer.Play();
        } else {
            _dialogueAudioPlayer.Stop();
        }
    }
}