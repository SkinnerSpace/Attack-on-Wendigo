using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference blizzardSFX;

    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = AudioPlayer.Create(blizzardSFX).WithVolume(0.5f);

        audioPlayer.WithVolume(0.5f);
        audioPlayer.PlayLoop();
        audioPlayer.WithVolume(0.5f);

        Logger.Print("Params " + audioPlayer.parametersCount);
    }
}
