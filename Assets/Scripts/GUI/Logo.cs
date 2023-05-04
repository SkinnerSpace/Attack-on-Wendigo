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
        
        container.onVisibilityUpdate += SetVolume;
    }

    private void Start()
    {
        Play();
        WaitAndFade();
    }

    private void Update()
    {
        Debug.Log(videoPlayer.frame);
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

    private void SetVolume(float value)
    {
        //Debug.Log("Value " + value);
    }

    private void IsOver(VideoPlayer player)
    {
        Debug.Log("IS over");
        gameObject.SetActive(false);
    }
}