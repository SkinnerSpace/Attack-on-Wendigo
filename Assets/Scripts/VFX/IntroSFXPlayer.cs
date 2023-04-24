using UnityEngine;

public class IntroSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference introWindSFX;

    private AudioPlayer introWindPlayer;

    private void Awake()
    {
        introWindPlayer = AudioPlayer.Create(introWindSFX);
    }

    private void Start()
    {
        GameEvents.current.onStart += () => introWindPlayer.PlayOneShot();
    }

}