using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioPlayer
{
    public void Play(FMODUnity.EventReference audioEvent)
    {
        int version = UnityEngine.Random.Range(0, 5);
        float pitch = UnityEngine.Random.Range(-2f, 2f);

        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(audioEvent);

        instance.setParameterByName("Versions", version);
        instance.setParameterByName("Pitch", pitch);

        instance.start();
        instance.release();
    }
}
