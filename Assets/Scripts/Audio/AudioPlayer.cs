using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioPlayer
{
    private const string PITCH = "Pitch";
    private const string VARIETY = "Versions";

    private Dictionary<string, float> parameters;

    private AudioPlayer()
    {
        parameters = new Dictionary<string, float>();
    }

    public static AudioPlayer Create()
    {
        return new AudioPlayer();
    }

    public AudioPlayer WithPitch(float min, float max)
    {
        parameters.Add(PITCH, Rand.Range(min, max));
        return this;
    }

    public AudioPlayer WithVariety(int variety)
    {
        return this;
    }

    public void Play(FMODUnity.EventReference audioReference)
    {
        AudioEvent audioEvent = new AudioEvent(audioReference);
        audioEvent.SetParameters(parameters);
        audioEvent.Play();
    }
}

public class AudioParam
{

}

public class AudioEvent
{
    private int version;

    FMOD.Studio.EventInstance instance;

    public AudioEvent(FMODUnity.EventReference audioReference)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(audioReference);
    }

    public void SetParameters(Dictionary<string, float> parameters)
    {
        foreach (string paramName in parameters.Keys)
        {
            instance.setParameterByName(paramName, parameters[paramName]);
        }
    }

    public void Play()
    {
        version = UnityEngine.Random.Range(0, 5);
        instance.setParameterByName("Versions", version);

        instance.start();
        instance.release();
    }
}
