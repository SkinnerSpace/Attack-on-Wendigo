using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSFXPlayer : MonoBehaviour
{
    private FMODUnity.EventReference thunderSFX;
    private AudioPlayer audioPlayer = new AudioPlayer();

    public void PlayThunderSFX()
    {
        audioPlayer.Play(thunderSFX);
    }
}
