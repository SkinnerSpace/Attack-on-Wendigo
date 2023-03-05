using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference stompSFX;

    [Header("Roars")]
    [SerializeField] private FMODUnity.EventReference arrivalRoarSFX;
    [SerializeField] private FMODUnity.EventReference arrivalBoneCrackSFX;
    [SerializeField] private FMODUnity.EventReference movementRoarSFX;

    [Header("Attack")]
    [SerializeField] private FMODUnity.EventReference fireballCastSFX;

    private AudioPlayer arrivalRoarAudioPlayer;
    private AudioPlayer arrivalBoneCrackAudioPlayer;
    private AudioPlayer stompAudioPlayer;
    private AudioPlayer shortRoarPlayer;

    private AudioPlayer fireballCastAudioPlayer;

    private void Awake()
    {
        arrivalRoarAudioPlayer = AudioPlayer.Create(arrivalRoarSFX).WithPitch(-2f, 2f);
        arrivalBoneCrackAudioPlayer = AudioPlayer.Create(arrivalBoneCrackSFX).WithPitch(-2f, 2f);
        stompAudioPlayer = AudioPlayer.Create(stompSFX).WithPitch(0f, 4f).WithVariety(3);
        shortRoarPlayer = AudioPlayer.Create(movementRoarSFX).WithPitch(-2f, 2f).WithVariety(3);

        fireballCastAudioPlayer = AudioPlayer.Create(fireballCastSFX).WithPitch(-3f, 3f);
    }

    public void PlayStompSFX(){
        stompAudioPlayer.WithPosition(transform.position).PlayOneShot();
    }

    public void PlayArrivalRoarSFX(){
        arrivalRoarAudioPlayer.WithPosition(transform.position).PlayOneShot();
    }

    public void PlayArrivalBoneCrackSFX(){
        arrivalBoneCrackAudioPlayer.WithPosition(transform.position).PlayOneShot();
    }

    public void PlayShortRoarSFX(){
        shortRoarPlayer.WithAnchor(transform).PlayOneShot();
    }

    public void PlayFireballCastSFX(Vector3 position){
        fireballCastAudioPlayer.WithPosition(position).PlayOneShot();
    }
}
