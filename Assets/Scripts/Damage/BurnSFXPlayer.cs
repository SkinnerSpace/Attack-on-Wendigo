using UnityEngine;

public class BurnSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference burnSFX;

    private AudioPlayer burnAudioPlayer;

    private void Awake()
    {
        burnAudioPlayer = AudioPlayer.Create(burnSFX).WithPosition(transform.position);
    }

    public void Play(){
        burnAudioPlayer.PlayLoop();
    }

    public void Stop(){
        burnAudioPlayer.Stop();
    }
}
