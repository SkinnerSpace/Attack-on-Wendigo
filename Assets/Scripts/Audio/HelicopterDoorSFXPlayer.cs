using UnityEngine;

public class HelicopterDoorSFXPlayer : MonoBehaviour, IHelicopterDoorObserver
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference doorSlideUpSFX;
    [SerializeField] private FMODUnity.EventReference doorSlideDownSFX;

    private HelicopterDoor door;

    private AudioPlayer doorSlideUpAudioPlayer;
    private AudioPlayer doorSlideDownAudioPlayer;

    private void Awake()
    {
        doorSlideUpAudioPlayer = AudioPlayer.Create(doorSlideUpSFX).WithAnchor(transform);
        doorSlideDownAudioPlayer = AudioPlayer.Create(doorSlideDownSFX).WithAnchor(transform);

        door = GetComponent<HelicopterDoor>();
        door.Subscribe(this);
    }

    public void OnDoorHasOpened() => PlayDoorSlideUpSFX();
    public void OnDoorHasClosed() => PlayDoorSlideDownSFX();

    public void PlayDoorSlideUpSFX() => doorSlideUpAudioPlayer.PlayOneShot();
    public void PlayDoorSlideDownSFX() => doorSlideDownAudioPlayer.PlayOneShot();
}

