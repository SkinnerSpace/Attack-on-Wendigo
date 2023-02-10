using UnityEngine;

public class CrateSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference dropSFX;
    private AudioPlayer dropAudioPlayer;

    private void Awake()
    {
        dropAudioPlayer = AudioPlayer.Create(dropSFX).WithAnchor(transform).WithPitch(-1f, 2f);
    }

    public void PlayDropSFX() => dropAudioPlayer.PlayOneShot();
}

