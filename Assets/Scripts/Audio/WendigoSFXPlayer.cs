using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference stompSFX;
    [SerializeField] private FMODUnity.EventReference arrivalRoarSFX;
    [SerializeField] private FMODUnity.EventReference movementRoarSFX;

    private AudioPlayer arrivalRoarAudioPlayer;
    private AudioPlayer stompAudioPlayer;

    private void Awake()
    {
        arrivalRoarAudioPlayer = AudioPlayer.Create(arrivalRoarSFX).WithPitch(-2f, 2f);
        stompAudioPlayer = AudioPlayer.Create(stompSFX).WithPitch(0f, 4f).WithVariety(3);
    }

    public void PlayStompSFX(){
        stompAudioPlayer.WithPosition(transform.position).PlayOneShot();
    }

    public void PlayArrivalRoarSFX(){
        arrivalRoarAudioPlayer.WithPosition(transform.position).PlayOneShot();
    }
}

