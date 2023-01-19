public abstract class AudioParam
{
    public abstract void ApplyTo(AudioEvent audioEvent);
}

public class AudioVolume : AudioParam
{
    private float volume;

    public AudioVolume(float volume) => this.volume = volume;

    public override void ApplyTo(AudioEvent audioEvent)
    {
        audioEvent.SetVolume(volume);
    }
}
