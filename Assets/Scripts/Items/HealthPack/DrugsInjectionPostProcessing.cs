using UnityEngine;

public class DrugsInjectionPostProcessing : MonoBehaviour
{
    private static int injectionAnimation = Animator.StringToHash("Inject");

    [SerializeField] private FMODUnity.EventReference heartBeatSFX;

    private Animator animator;
    private AudioPlayer heartBeatAudioPlayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        heartBeatAudioPlayer = AudioPlayer.Create(heartBeatSFX);
    }

    private void Start()
    {
        PlayerEvents.current.onHealthRestore += Play;
    }

    private void Play()
    {
        animator.Play(injectionAnimation, -1, 0f);
        heartBeatAudioPlayer.PlayOneShot();
    }
}