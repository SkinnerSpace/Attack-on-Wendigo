using UnityEngine;

public class AudioPlayer
{
    public int parametersCount => parameters.Count;

    private AudioEvent audioEvent;
    private FMODUnity.EventReference audioReference;
    private AudioParameters parameters;

    public static AudioPlayer Create(FMODUnity.EventReference audioReference) => new AudioPlayer(audioReference);

    private AudioPlayer(FMODUnity.EventReference audioReference)
    {
        this.audioReference = audioReference;
        parameters = new AudioParameters();

    }

    public AudioPlayer WithPitch(float min, float max)
    {
        parameters.Get(AudioPitch.parameter).Set(min, max);
        return this;
    }

    public AudioPlayer WithVariety(int variety)
    {
        parameters.Get(AudioVariety.parameter).Set(variety);
        return this;
    }

    public AudioPlayer WithVolume(float volume)
    {
        parameters.Get(AudioVolume.parameter).Set(volume);
        return this;
    }

    public AudioPlayer WithPosition(Vector3 position)
    {
        parameters.Get(AudioPosition.parameter).Set(position);
        return this;
    }

    public AudioPlayer WithStartTime(float timelinePosition)
    {
        parameters.Get(AudioTimelinePosition.parameter).Set(timelinePosition);
        return this;
    }

    public void PlayOneShot()
    {
        audioEvent = new AudioEvent(audioReference);
        parameters.ApplyTo(audioEvent);
        audioEvent.PlayOneShot();
    }

    public void PlayLoop()
    {
        audioEvent = new AudioEvent(audioReference);
        parameters.ApplyTo(audioEvent);
        audioEvent.PlayLoop();
    }

    public void Update()
    {
        if (audioEvent != null)
            parameters.ApplyTo(audioEvent);
    }

    public void Stop() => audioEvent.Stop();
}
