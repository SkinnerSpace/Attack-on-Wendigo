using System;
using UnityEngine;

public class AudioPlayer
{
    private const float MUTE_TIME = 8f;

    private static event Action onPause;
    private static event Action onResume;

    private AudioEvent audioEvent;
    private FMODUnity.EventReference audioReference;
    private AudioParameters parameters;

    private bool unpausable;
    private bool isLoop;
    private float timeToDisappear;
    private bool isPaused;

    public static AudioPlayer Create(FMODUnity.EventReference audioReference) => new AudioPlayer(audioReference);

    private AudioPlayer(FMODUnity.EventReference audioReference)
    {
        this.audioReference = audioReference;
        parameters = new AudioParameters();
    }

    ~AudioPlayer(){
        UnsubscribeFromPauseAndResume();
        AudioPlayersManager.Instance.UnsubscribeFromTimeUpdate(DisappearOnTime);
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

    public AudioPlayer SetUnpausable()
    {
        unpausable = true;
        return this;
    }

    public void PlayOneShot()
    {
        UnsubscribeFromPauseAndResume();

        audioEvent = new AudioEvent(audioReference);
        parameters.ApplyTo(audioEvent);
        audioEvent.PlayOneShot();

        SubscribeOnPauseAndResume();

        SetDisappearanceTimer();
    }

    public void PlayLoop()
    {
        UnsubscribeFromPauseAndResume();

        isLoop = true;
        audioEvent = new AudioEvent(audioReference);
        parameters.ApplyTo(audioEvent);
        audioEvent.PlayLoop();

        SubscribeOnPauseAndResume();
    }

    public void Update()
    {
        if (audioEvent != null){
            parameters.ApplyTo(audioEvent);
        }

        ResetDisappearanceTimer();
    }

    public void Stop()
    {
        if (audioEvent != null){
            audioEvent.Stop();
        }

        UnsubscribeFromPauseAndResume();
        AudioPlayersManager.Instance.UnsubscribeFromTimeUpdate(DisappearOnTime);
    }

    public void DisappearOnTime(float time)
    {
        if (time >= timeToDisappear && !isPaused){
            UnsubscribeFromPauseAndResume();
            AudioPlayersManager.Instance.UnsubscribeFromTimeUpdate(DisappearOnTime);
        }
    }

    private void SubscribeOnPauseAndResume(){
        if (!unpausable){
            onPause += Pause;
            onResume += Resume;
        }
    }

    public void UnsubscribeFromPauseAndResume(){
        if (!unpausable){
            onPause -= Pause;
            onResume -= Resume;

            audioEvent = null;
        }
    }

    private void Pause(){
        if (audioEvent != null){
            isPaused = true;
            audioEvent.Pause();
        }
    }

    private void Resume(){
        if (audioEvent != null){
            ResetDisappearanceTimer();
            isPaused = false;
            audioEvent.Resume();
        }
    }

    public static void PauseAll() => onPause?.Invoke();

    public static void ResumeAll() => onResume?.Invoke();

    private void SetDisappearanceTimer(){
        if (!isLoop){
            AudioPlayersManager.Instance.SubscribeOnTimeUpdate(DisappearOnTime);
            timeToDisappear = Time.realtimeSinceStartup + MUTE_TIME;
        }
    }

    private void ResetDisappearanceTimer(){
        if (!isLoop){
            timeToDisappear = Time.realtimeSinceStartup + MUTE_TIME;
        }
    } 
}
