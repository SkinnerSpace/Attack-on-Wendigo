using UnityEngine;
using System;

public class CollapseSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference fallSFX;

    private AudioPlayer fallAudioPlayer;
    private CollapseController controller;

    private void Awake()
    {
        fallAudioPlayer = AudioPlayer.Create(fallSFX).WithPitch(-3f, 3f).WithStartTime(Rand.Range(0f, 1f)).WithPosition(transform.position);
    }

    public void PlayFallSFX() => fallAudioPlayer.PlayLoop();

    public void SubscribeTo(CollapseController controller)
    {
        this.controller = controller;
        controller.notifyOnUpdate += TimeOut;
    }

    public void TimeOut(float completeness)
    {
        if (completeness >= 0.6f)
        {
            controller.notifyOnUpdate -= TimeOut;
            fallAudioPlayer.Stop();
        }
    }
}
