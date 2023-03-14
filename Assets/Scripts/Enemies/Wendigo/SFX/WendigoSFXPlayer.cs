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

    [Header("Firebreath")]
    [SerializeField] private FMODUnity.EventReference inhaleSFX;
    [SerializeField] private FMODUnity.EventReference exhaleSFX;

    [Header("Sound Sources")]
    [SerializeField] private Transform head;

    private AudioPlayer arrivalRoarAudioPlayer;
    private AudioPlayer arrivalBoneCrackAudioPlayer;
    private AudioPlayer stompAudioPlayer;
    private AudioPlayer shortRoarPlayer;

    private AudioPlayer fireballCastAudioPlayer;

    private AudioPlayer inhalePlayer;
    private AudioPlayer exhalePlayer;

    private void Awake()
    {
        arrivalRoarAudioPlayer = AudioPlayer.Create(arrivalRoarSFX).WithAnchor(head).WithPitch(-2f, 2f);
        arrivalBoneCrackAudioPlayer = AudioPlayer.Create(arrivalBoneCrackSFX).WithPitch(-2f, 2f);

        stompAudioPlayer = AudioPlayer.Create(stompSFX).WithPitch(0f, 4f).WithVariety(3);

        shortRoarPlayer = AudioPlayer.Create(movementRoarSFX).WithAnchor(head).WithPitch(-2f, 2f).WithVariety(3);

        fireballCastAudioPlayer = AudioPlayer.Create(fireballCastSFX).WithPitch(-3f, 3f);

        inhalePlayer = AudioPlayer.Create(inhaleSFX).WithAnchor(head).WithPitch(-1f, 1f).WithVariety(2);
        exhalePlayer = AudioPlayer.Create(exhaleSFX).WithAnchor(head).WithPitch(-1f, 1f).WithVariety(2);
    }

    public void PlayStompSFX(Vector3 position){
        stompAudioPlayer.WithPosition(position).PlayOneShot();
    }

    public void PlayArrivalRoarSFX(){
        arrivalRoarAudioPlayer.PlayOneShot();
    }

    public void PlayArrivalBoneCrackSFX(){
        arrivalBoneCrackAudioPlayer.WithPosition(transform.position).PlayOneShot();
    }

    public void PlayShortRoarSFX(){
        shortRoarPlayer.PlayOneShot();
    }

    public void PlayFireballCastSFX(Vector3 position){
        fireballCastAudioPlayer.WithPosition(position).PlayOneShot();
    }

    public void PlayInhaleSFX(){
        inhalePlayer.PlayOneShot();
    }

    public void PlayExhaleSFX(){
        exhalePlayer.PlayOneShot();
    }
}