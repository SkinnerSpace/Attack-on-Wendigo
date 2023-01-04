using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [SerializeField] private FMODUnity.EventReference testEvent;
    private FunctionTimer timer;

    private void Awake()
    {
        //timer = new GameObject("Timer").AddComponent<FunctionTimer>().GetComponent<FunctionTimer>();
    }

    private void Start()
    {
        //timer.Set("Play sound", 1f, PlaySound);
    }

    private void PlaySound()
    {
        int version = UnityEngine.Random.Range(0, 5);
        float pitch = UnityEngine.Random.Range(-2f, 2f);

        instance = FMODUnity.RuntimeManager.CreateInstance(testEvent);

        instance.setParameterByName("Versions", version);
        instance.setParameterByName("Pitch", pitch);

        instance.start();
        instance.release();

        timer.Set("Play sound", 1f, PlaySound);
    }
}
