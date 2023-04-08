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

    private bool isUnpausable;
    private bool isPaused;
    private bool isLoop;
    private float timeToDisappear;

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

    // Customization

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

    public AudioPlayer WithRandomStartTime()
    {
        parameters.Get<AudioTimelinePosition>().Set(Rand.Range(0f, 1f));
        return this;
    }

    public AudioPlayer WithUnpausableMode()
    {
        isUnpausable = true;
        return this;
    }

    // Play

    public void PlayOneShot()
    {
        AssembleAndRegisterAudioEvent();

        audioEvent.PlayOneShot();
        SetDisappearanceTimer();
    }

    public void PlayLoop()
    {
        AssembleAndRegisterAudioEvent();

        isLoop = true;
        audioEvent.PlayLoop();
    }

    private void AssembleAndRegisterAudioEvent()
    {
        UnsubscribeFromPauseAndResume();
        AssembleAudioEvent();
        SubscribeOnPauseAndResume();
    }

    private void AssembleAudioEvent(){
        audioEvent = new AudioEvent(audioReference);
        parameters.ApplyTo(audioEvent);
    }

    public void UpdateParameters()
    {
        ApplyParametersIfEventExist();
        ResetDisappearanceTimer();
    }

    private void ApplyParametersIfEventExist()
    {
        if (audioEvent != null){
            parameters.ApplyTo(audioEvent);
        }
    }

    public void Stop()
    {
        StopAudioEvent();
        UnsubscribeFromPauseAndResume();
        UnsubscribeFromTimeUpdate();
    }

    private void StopAudioEvent()
    {
        if (audioEvent != null){
            audioEvent.Stop();
        }
    }

    // Pause and resume

    private void SubscribeOnPauseAndResume(){
        if (!isUnpausable){
            onPause += Pause;
            onResume += Resume;
        }
    }

    public void UnsubscribeFromPauseAndResume(){
        if (!isUnpausable){
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

    // Disappearance timer

    private void SetDisappearanceTimer(){
        if (!isLoop){
            SubscribeOnTimeUpdate();
            timeToDisappear = Time.realtimeSinceStartup + MUTE_TIME;
        }
    }

    private void ResetDisappearanceTimer(){
        if (!isLoop){
            timeToDisappear = Time.realtimeSinceStartup + MUTE_TIME;
        }
    } 

    private void SubscribeOnTimeUpdate() => AudioPlayersManager.Instance.SubscribeOnTimeUpdate(DisappearOnTime);
    private void UnsubscribeFromTimeUpdate() => AudioPlayersManager.Instance.UnsubscribeFromTimeUpdate(DisappearOnTime);

    public void DisappearOnTime(float time)
    {
        if (TimeOutDuringGameplay(time))
        {
            UnsubscribeFromPauseAndResume();
            UnsubscribeFromTimeUpdate();
        }
    }

    private bool TimeOutDuringGameplay(float time)
    {
        return time >= timeToDisappear &&
               !isPaused;
    }
}
