using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardSFXPlayer : MonoBehaviour
{
    [SerializeField] private Blizzard blizzard;
    [SerializeField] private Transform attenuationPoint;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference blizzardSFX;

    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = AudioPlayer.Create(blizzardSFX);
        UpdateAttenuation();

        audioPlayer.PlayLoop();
    }

    private void Update() => UpdateAttenuation();

    private void UpdateAttenuation()
    {
        float distToCenter = Vector3.Distance(transform.position, attenuationPoint.position);
        float volume = Mathf.InverseLerp(0f, blizzard.Radius, distToCenter);
        volume = Easing.QuadEaseIn(volume);

        audioPlayer.WithVolume(volume).UpdateParameters();
    }
}
