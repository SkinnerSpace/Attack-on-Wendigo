﻿using UnityEngine;

public class HelicopterSFXPlayer : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Transform anchor;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference bladesSFX;

    [Header("Settings")]
    [SerializeField] private float menuVolume;

    private AudioPlayer bladesAudioPlayer;

    private void Awake()
    {
        bladesAudioPlayer = AudioPlayer.Create(bladesSFX).WithAnchor(anchor).WithVolume(menuVolume);
    }

    private void Start()
    {
        MenuEvents.current.onMainMenuOpenedForTheFirstTime += () => bladesAudioPlayer.PlayLoop();
        GameEvents.current.onIntroIsOver += IncreaseVolume;
    }

    private void IncreaseVolume(){
        bladesAudioPlayer.WithVolume(1f).UpdateParameters();
    }
}

