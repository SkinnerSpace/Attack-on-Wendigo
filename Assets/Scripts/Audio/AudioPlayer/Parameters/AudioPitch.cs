public class AudioPitch : AudioParam
{
    private float pitch;

    private float min;
    private float max;

    public AudioPitch(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public override void ApplyTo(AudioEvent audioEvent)
    {
        Update();
        audioEvent.SetPitch(pitch);
    }

    private void Update() => pitch = Rand.Range(min, max);
}
