using UnityEngine;
using System.Collections;

public class FootstepsSFXPlayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterData data;
    [SerializeField] private Chronos chronos;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference snowFootstepSFX;
    [SerializeField] private FMODUnity.EventReference concreteFootStepSFX;

    private AudioPlayer audioPlayer;
    private Walker walker;

    private void Awake()
    {
        audioPlayer = AudioPlayer.Create(snowFootstepSFX).WithPitch(-2f, 2f).WithVariety(5);
        walker = new Walker(data, chronos);
    }

    private void Update()
    {
        walker.Walk(PlaySFX);
    }

    private void PlaySFX()
    {
        audioPlayer.PlayOneShot();
    }
}
