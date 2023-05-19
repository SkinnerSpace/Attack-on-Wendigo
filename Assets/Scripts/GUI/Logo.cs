using UnityEngine;
using UnityEngine.Video;

public class Logo : MonoBehaviour
{
    private const float time = 3.15f;
    private static bool isPlayed;


    [SerializeField] private FMODUnity.EventReference logoSFX;

    private GUIContainer container;
    private FunctionTimer timer;
    private VideoPlayer videoPlayer;
    private AudioPlayer logoAudioPlayer;

    private void Awake()
    {
        container = GetComponent<GUIContainer>();
        timer = GetComponent<FunctionTimer>();
        videoPlayer = GetComponentInChildren<VideoPlayer>();

        logoAudioPlayer = AudioPlayer.Create(logoSFX);

        videoPlayer.loopPointReached += IsOver;
    }

    private void Start()
    {
        Play();
        WaitAndFade();
    }

    private void Play()
    {
        if (!isPlayed){
            isPlayed = true;

            videoPlayer.Play();
            logoAudioPlayer.PlayOneShot();
        }
    }

    private void WaitAndFade()
    {
        timer.Set("FadeOut", time, () => container.HideGradually());
    }

    private void IsOver(VideoPlayer player)
    {
        gameObject.SetActive(false);
    }
}