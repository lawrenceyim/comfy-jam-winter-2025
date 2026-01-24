using Godot;
using RepositorySystem;
using ServiceSystem;

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
    private AudioStreamPlayer _sfxPlayer;

    private SoundEffectRepository _soundEffectRepository;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _soundEffectRepository = repositoryLocator.GetRepository<SoundEffectRepository>(RepositoryName.SoundEffect);

        PlayAnimation(AnimalAnimation.Idle);
        DisplaySpeechBubble(false);
    }

    public void DisplaySpeechBubble(bool displayed) {
        _speechBubble.Visible = displayed;
        if (displayed) {
            _speechBubble.Play();
            _PlayDialogueAudio(true);
        } else {
            _PlayDialogueAudio(false);
        }
    }

    public void PlayEatingSfx(bool play) {
        if (play) {
            _sfxPlayer.VolumeDb = -5;
            _sfxPlayer.Stream = _soundEffectRepository.GetSoundEffect(SoundEffectId.FoxEat);
            _sfxPlayer.Play();
        } else {
            _sfxPlayer.Stop();
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
            _sfxPlayer.VolumeDb = -10;
            _sfxPlayer.Stream = _soundEffectRepository.GetSoundEffect(SoundEffectId.FoxTalk);
            _sfxPlayer.Play();
        } else {
            _sfxPlayer.Stop();
        }
    }
}