using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent
{
    private static event Action onStop;
    private static event Action onPause;
    private static event Action onResume;

    public FMOD.Studio.EventInstance instance { get; private set; }
    public AudioEvent(FMODUnity.EventReference audioReference)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(audioReference);
    }

    public void SetVariant(int variant) => instance.setParameterByName("Versions", variant);
    public void SetPitch(float pitch) => instance.setParameterByName("Pitch", pitch);
    public void SetVolume(float volume) => instance.setParameterByName("Vol", volume);
    public void SetTimelinePosition(int milliseconds) => instance.setTimelinePosition(milliseconds);
    public void Set3DPosition(Vector3 position) => instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
    public void SetAnchor(Transform body) => FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, body);

    public int GetLength(){
        FMOD.Studio.EventDescription description = GetDescription();
        description.getLength(out int length);
        return length;
    }

    public FMOD.Studio.EventDescription GetDescription(){
        instance.getDescription(out FMOD.Studio.EventDescription description);
        return description;
    }

    public void PlayOneShot()
    {
        instance.start();
        instance.release();
    }
    
    public void PlayLoop()
    {
        instance.start();

/*        InitializeIfNecessary();*/

        onStop += ForcedStop;
        onPause += Pause;
/*        audioEvents.Add(this);
        Debug.Log(audioEvents.Count);*/
    }

    private void ForcedStop()
    {
        Stop();
        Debug.Log("Forced Stop");
    }

    public void Stop()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();

        onStop -= Stop;

        /*        InitializeIfNecessary();*/
        /*        audioEvents.Remove(this);
                Debug.Log(audioEvents.Count);*/
    }

    public void Pause()
    {
        instance
    }

    public static void StopAll() => onStop?.Invoke();

    public static void PauseAll() => onPause?.Invoke();

    public static void ResumeAll() => onResume?.Invoke();
}
