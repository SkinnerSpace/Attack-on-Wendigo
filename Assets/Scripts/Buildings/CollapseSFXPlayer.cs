using UnityEngine;

public class CollapseSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference fallSFX;

    private AudioPlayer fallAudioPlayer;

    private void Awake()
    {
        fallAudioPlayer = AudioPlayer.Create(fallSFX).WithPitch(-3f, 3f).WithStartTime(Rand.Range(0f, 1f)).WithPosition(transform.position);
    }

    public void PlayFallSFX() => fallAudioPlayer.PlayLoop();
    public void Stop() => fallAudioPlayer.Stop();
}
