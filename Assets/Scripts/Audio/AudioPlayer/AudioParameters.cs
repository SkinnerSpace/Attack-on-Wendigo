using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioParameters
{
    public int Count => parameters.Count;
    private Dictionary<Type, AudioParam> parameters;

    public AudioParameters() => parameters = new Dictionary<Type, AudioParam>();

    public AudioParam Get(Type parameter)
    {
        if (NoInstanceOf(parameter))
            parameters.Add(parameter, Create(parameter));

        return parameters[parameter];
    }

    public void ApplyTo(AudioEvent audioEvent)
    {
        foreach (AudioParam param in parameters.Values)
        {
            //Debug.Log(param.ToString());
            param.ApplyTo(audioEvent);
        }
    }

    private bool NoInstanceOf(Type parameter) => !parameters.ContainsKey(parameter);

    private AudioParam Create(Type parameter)
    {
        if (parameter == AudioPitch.parameter) return new AudioPitch();
        if (parameter == AudioVariety.parameter) return new AudioVariety();
        if (parameter == AudioVolume.parameter) return new AudioVolume();
        if (parameter == AudioPosition.parameter) return new AudioPosition();

        return null;
    }
}