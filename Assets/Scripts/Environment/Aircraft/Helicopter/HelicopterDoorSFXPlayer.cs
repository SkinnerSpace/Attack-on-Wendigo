using UnityEngine;

public class HelicopterDoorSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference doorSlideUpSFX;
    [SerializeField] private FMODUnity.EventReference doorSlideDownSFX;

    private AudioPlayer doorSlideUpAudioPlayer;
    private AudioPlayer doorSlideDownAudioPlayer;

    private void Awake()
    {
        doorSlideUpAudioPlayer = AudioPlayer.Create(doorSlideUpSFX).WithAnchor(transform);
        doorSlideDownAudioPlayer = AudioPlayer.Create(doorSlideDownSFX).WithAnchor(transform);
    }

    public void PlayDoorSlideUpSFX() => doorSlideUpAudioPlayer.PlayOneShot();

    public void PlayDoorSlideDownSFX() => doorSlideDownAudioPlayer.PlayOneShot();
}

