using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioPlayer
{
    private AudioEvent audioEvent;
    private List<AudioParam> parameters;

    public static AudioPlayer Create(FMODUnity.EventReference audioReference)
    {
        return new AudioPlayer(audioReference);
    }

    private AudioPlayer(FMODUnity.EventReference audioReference)
    {
        audioEvent = new AudioEvent(audioReference);
        parameters = new List<AudioParam>();
    }

    public AudioPlayer WithPitch(float min, float max)
    {
        parameters.Add(new AudioPitch(min, max));
        return this;
    }

    public AudioPlayer WithVariety(int variety)
    {
        parameters.Add(new AudioVariety(variety));
        return this;
    }

    public AudioPlayer WithVolume(float volume)
    {
        parameters.Add(new AudioVolume(volume));
        return this;
    }

    public AudioPlayer WithPosition(Vector3 position)
    {
        parameters.Add(new AudioPosition(position));
        return this;
    }

    public void PlayOneShot()
    {
        ApplyParameters();
        audioEvent.PlayOneShot();
    }

    public void PlayLoop()
    {
        ApplyParameters();
        audioEvent.PlayLoop();
    }

    private void ApplyParameters()
    {
        foreach (AudioParam param in parameters)
            param.ApplyTo(audioEvent);
    }
}
