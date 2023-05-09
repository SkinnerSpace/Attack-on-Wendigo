using System;
using UnityEngine;

public class AudioPitch : AudioParam
{
    public new static Type parameter => typeof(AudioPitch); 

    private float pitch;
    private float previousPitch;

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

    private void Update()
    {
        float rangePos = Mathf.InverseLerp(min, max, previousPitch);

        float currentMin = min;
        float currentMax = max;

        if (rangePos <= 0.5f)
        {
            currentMin = Mathf.InverseLerp(min, max, rangePos * 2f);
        }
        else if (rangePos > 0.5f)
        {
            currentMax = Mathf.InverseLerp(min, max, rangePos / 2f);
        }

        pitch = Rand.Range(currentMin, currentMax);
        previousPitch = pitch;
    }

    public override string ToString() => $"Pitch, min: {min}, max: {max}, Current: {pitch}";
}
