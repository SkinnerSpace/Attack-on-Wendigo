using System;
using UnityEngine;

public class AudioTimelinePosition : AudioParam
{
    public new static Type parameter => typeof(AudioTimelinePosition);

    private float startingTime;
    private bool applied;

    public override void Set(float startingTime){
        this.startingTime = startingTime;
        applied = false;
    }

    public override void ApplyTo(AudioEvent audioEvent)
    {
        if (!applied){
            applied = true;

            float length = audioEvent.GetLength();
            int milliseconds = (int)(length * startingTime);

            audioEvent.SetTimelinePosition(milliseconds);
        }
    }
}