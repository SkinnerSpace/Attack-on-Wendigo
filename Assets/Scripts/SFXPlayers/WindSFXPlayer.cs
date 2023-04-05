using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference windSFX;

    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = AudioPlayer.Create(windSFX).SetUnpausable();
        audioPlayer.PlayLoop();
    }
}
