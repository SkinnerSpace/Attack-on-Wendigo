using System;
using System.Collections.Generic;

public class AudioParameters
{
    private List<AudioParam> parameters;

    public AudioParameters() => parameters = new List<AudioParam>();

    public T Get<T>() where T : AudioParam
    {
        foreach (AudioParam audioParam in parameters)
        {
            if (audioParam is T) 
                return audioParam as T;
        }

        parameters.Add(Activator.CreateInstance<T>());
        return parameters[parameters.Count - 1] as T;
    }

    public void ApplyTo(AudioEvent audioEvent)
    {
        foreach (AudioParam param in parameters)
            param.ApplyTo(audioEvent);
    }
}