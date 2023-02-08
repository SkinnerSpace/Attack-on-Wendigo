using UnityEngine;

public class HelicopterSFXPlayer : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Transform anchor;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference bladesSFX;

    private AudioPlayer bladesAudioPlayer;

    private void Awake()
    {
        bladesAudioPlayer = AudioPlayer.Create(bladesSFX).WithAnchor(anchor);
        bladesAudioPlayer.PlayLoop();
    }
}

