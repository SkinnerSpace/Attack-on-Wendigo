using UnityEngine;

public class AudioPlayer
{
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
        parameters.Get<AudioPitch>().Set(min, max);
        return this;
    }

    public AudioPlayer WithVariety(int variety)
    {
        parameters.Get<AudioVariety>().Set(variety);
        return this;
    }

    public AudioPlayer WithVolume(float volume)
    {
        parameters.Get<AudioVolume>().Set(volume);
        return this;
    }

    public AudioPlayer WithPosition(Vector3 position)
    {
        parameters.Get<AudioPosition>().Set(position);
        return this;
    }

    public AudioPlayer WithAnchor(Transform body)
    {
        parameters.Get<AudioAnchor>().Set(body);
        return this;
    }

    public AudioPlayer WithStartTime(float timelinePosition)
    {
        parameters.Get<AudioTimelinePosition>().Set(timelinePosition);
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
        if (audioEvent != null){
            parameters.ApplyTo(audioEvent);
        }
    }

    public void Stop()
    {
        if (audioEvent != null){
            audioEvent.Stop();
        }
    }
}
