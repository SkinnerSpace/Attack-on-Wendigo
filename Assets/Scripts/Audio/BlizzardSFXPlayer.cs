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
        audioPlayer = AudioPlayer.Create(blizzardSFX);
        UpdateAttenuation();

        audioPlayer.PlayLoop();
    }

    private void Update() => UpdateAttenuation();

    private void UpdateAttenuation()
    {
        float distToCenter = Vector3.Distance(transform.position, PlayerCharacter.Instance.position);
        float distToRadius = Mathf.Min((distToCenter / Blizzard.Instance.Radius), 1f); 
        float volume = distToRadius;

        audioPlayer.WithVolume(volume);
        audioPlayer.Update();
    }
}
