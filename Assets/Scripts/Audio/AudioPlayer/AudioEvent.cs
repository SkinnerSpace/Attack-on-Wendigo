using System.Collections.Generic;
using UnityEngine;

public class AudioEvent
{
    private FMODUnity.EventReference audioReference;
    public FMOD.Studio.EventInstance instance { get; private set; }

    public AudioEvent(FMODUnity.EventReference audioReference)
    {
        this.audioReference = audioReference;
        instance = FMODUnity.RuntimeManager.CreateInstance(audioReference);
    }

    public void SetVariant(int variant) => instance.setParameterByName("Versions", variant);
    public void SetPitch(float pitch) => instance.setParameterByName("Pitch", pitch);
    public void SetVolume(float volume) => instance.setVolume(volume);
    public void Set3DPosition(Vector3 position) => instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));

    public void PlayOneShot()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(audioReference);

        instance.start();
        instance.release();
    }
    
    public void PlayLoop()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(audioReference);
        instance.start();
    }
}
