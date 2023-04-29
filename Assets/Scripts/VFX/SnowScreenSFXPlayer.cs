using UnityEngine;

public class SnowScreenSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference windSFX;

    private AudioPlayer windPlayer;

    private void Awake()
    {
        windPlayer = AudioPlayer.Create(windSFX);
    }

    public void Play()
    {
        windPlayer.PlayOneShot();
    }
}