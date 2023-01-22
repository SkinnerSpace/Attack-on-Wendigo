using UnityEngine;
using System;

public class CollapseSFXPlayer : MonoBehaviour, ICollapseObserver
{
    private const float TIMEOUT = 0.6f;

    [Header("Requited Components")]
    [SerializeField] private CollapseController controller;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference fallSFX;

    private AudioPlayer fallAudioPlayer;
    private bool isPlaying;

    private void Awake()
    {
        controller.Subscribe(this);
        fallAudioPlayer = AudioPlayer.Create(fallSFX).WithPitch(-3f, 3f).WithStartTime(Rand.Range(0f, 1f)).WithPosition(transform.position);
    }

    public void ReceiveCollapseUpdate(float completeness)
    {
        if (!isPlaying) Play();
        if (completeness >= TIMEOUT) Stop();
    }

    private void Play()
    {
        isPlaying = true;
        fallAudioPlayer.PlayLoop();
    }

    private void Stop()
    {
        controller.notifyOnUpdate -= ReceiveCollapseUpdate;
        fallAudioPlayer.Stop();
    }
}

public interface ICollapseObserver
{
    void ReceiveCollapseUpdate(float completeness);
}