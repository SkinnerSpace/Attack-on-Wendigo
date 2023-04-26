using UnityEngine;

public class AdditionalSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference victorySFX;
    private AudioPlayer victoryAudioPlayer;

    private void Awake()
    {
        victoryAudioPlayer = AudioPlayer.Create(victorySFX);
    }

    private void Start()
    {
        GameEvents.current.onVictory += () => victoryAudioPlayer.PlayOneShot();
    }
}