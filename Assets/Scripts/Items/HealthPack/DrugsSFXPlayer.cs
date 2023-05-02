using UnityEngine;

public class DrugsSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference injectionSFX;

    private AudioPlayer injectionPlayer;

    private void Awake()
    {
        injectionPlayer = AudioPlayer.Create(injectionSFX);
    }

    public void PlayInjectionSFX() => injectionPlayer.PlayOneShot();
}