using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LogoIntroController : MonoBehaviour
{
    private const string NOTIFY_ON_START_TIMER = "NotifyOnStart";
    private const string APPEAR_TIMER = "Appear";
    private const string FADEOUT_TIMER = "FadeOut";

    [SerializeField] private float appearTime;
    [SerializeField] private float visibleTime;
    [SerializeField] private FMODUnity.EventReference logoSFX;

    [SerializeField] private GUIContainer logoImage;
    [SerializeField] private VideoPlayer videoPlayer;

    private GUIContainer container;
    private FunctionTimer timer;
    private AudioPlayer audioPlayer;

    private bool isPlaying;

    private void Awake()
    {
        audioPlayer = AudioPlayer.Create(logoSFX);

        container = GetComponent<GUIContainer>();
        timer = GetComponent<FunctionTimer>();
    }

    void Start()
    {
        if (GlobalData.showIntroOnStart)
        {
            PlayOnStart();
        }
        else
        {
            HideOnStart();
        }
    }

    private void PlayOnStart()
    {
        videoPlayer.Play();
    }

    private void HideOnStart()
    {
        container.HideImmediately();
        timer.Set(NOTIFY_ON_START_TIMER, 0.1f, () => MenuEvents.current.NotifyOnMainMenuOpenedForTheFirstTime());
    }

    private void Update()
    {
        AppearOnAnimationStartPlaying();
        SkipLogo();
    }

    private void AppearOnAnimationStartPlaying()
    {
        if (GlobalData.showIntroOnStart && videoPlayer.isPlaying)
        {
            GlobalData.showIntroOnStart = false;

            audioPlayer.PlayOneShot();
            timer.Set(APPEAR_TIMER, appearTime, ShowLogo);
            timer.Set(FADEOUT_TIMER, visibleTime, FadeOut);
        }
    }

    private void SkipLogo()
    {
        if (isPlaying && Input.anyKeyDown)
        {
            Stop();
        }
    }

    private void ShowLogo()
    {
        logoImage.ShowGradually();
        isPlaying = true;
    }

    private void Stop()
    {
        audioPlayer.Stop();
        timer.Stop(APPEAR_TIMER);
        timer.Stop(FADEOUT_TIMER);
        FadeOut();
    }


    private void FadeOut()
    {
        isPlaying = false;
        container.HideGradually();
        MenuEvents.current.NotifyOnMainMenuOpenedForTheFirstTime();
    }
}
