using System;
using UnityEngine;

public class AudioVolume : AudioParam
{
    public new static Type parameter => typeof(AudioVolume);
    private float volume;

    public override void Set(float volume) => this.volume = volume;
    public override void ApplyTo(AudioEvent audioEvent) => audioEvent.SetVolume(volume);

    public override string ToString() => $"Volume: {volume}";
}
