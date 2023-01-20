using System.Collections.Generic;
using UnityEngine;

public class AudioEvent
{
    public FMOD.Studio.EventInstance instance { get; private set; }

    public AudioEvent(FMODUnity.EventReference audioReference) => instance = FMODUnity.RuntimeManager.CreateInstance(audioReference);

    public void SetVariant(int variant) => instance.setParameterByName("Versions", variant);
    public void SetPitch(float pitch) => instance.setParameterByName("Pitch", pitch);
    public void SetVolume(float volume) => instance.setParameterByName("Vol", volume);
    public void Set3DPosition(Vector3 position) => instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));

    public void PlayOneShot()
    {
        instance.start();
        instance.release();
    }
    
    public void PlayLoop()
    {
        instance.getParameterByName("Vol", out float vol);
        instance.start();
    }
}
