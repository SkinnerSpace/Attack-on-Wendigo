using System;

public class AudioPitch : AudioParam
{
    public new static Type parameter => typeof(AudioPitch); 

    private float pitch;

    private float min;
    private float max;

    public override void Set(float min, float max)
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

    public override string ToString() => $"Pitch, min: {min}, max: {max}, Current: {pitch}";
}
