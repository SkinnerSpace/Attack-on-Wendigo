using UnityEngine;

public class MessageSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference appearSFX;
    [SerializeField] private FMODUnity.EventReference fadeOutSFX;

    private AudioPlayer appearAudioPlayer;
    private AudioPlayer fadeOutPlayer;

    private void Awake()
    {
        appearAudioPlayer = AudioPlayer.Create(appearSFX).WithPitch(-1f, 1f);
        fadeOutPlayer = AudioPlayer.Create(fadeOutSFX).WithPitch(-1f, 1f);
    }

    public void PlayAppearSFX() => appearAudioPlayer.PlayOneShot();
    public void PlayFadeOutSFX() => fadeOutPlayer.PlayOneShot();
}
