using UnityEngine;

public class IntroScreen : MonoBehaviour
{
    private enum States
    {
        None,
        FadeIn,
        FadeOut
    }

    [Header("Required Components")]
    [SerializeField] private CanvasGroup whiteScreen;
    [SerializeField] private ParticleSystem snow;
    [SerializeField] private BlizzardPostProcessingController postProcessingController;
    [SerializeField] private FunctionTimer timer;

    [Header("Time")]
    [SerializeField] private float fadeInTime = 2f;
    [SerializeField] private float delayTime = 0.1f;
    [SerializeField] private float fadeOutTime = 2f;

    private States state;

    private float time;
    private float lerp;

    private void Start(){
        GameEvents.current.onStart += FadeIn;
    }

    private void Update()
    {
        if (state == States.FadeIn){
            UpdateFadeIn();
        }
        else if (state == States.FadeOut){
            UpdateFadeOut();
        }
    }

    private void FadeIn()
    {
        snow.Play();
        state = States.FadeIn;
    }

    private void UpdateFadeIn()
    {
        time += Time.deltaTime;
        lerp = Mathf.InverseLerp(0f, fadeInTime, time);
        lerp = Easing.QuadEaseIn(lerp);

        if (lerp >= 1f){
            state = States.None;
            timer.Set("FadeOut", delayTime, FadeOut);
            GameEvents.current.TheGameHasBegun();
        }

        UpdateVFX();
    }

    private void FadeOut()
    {
        snow.Stop();
        state = States.FadeOut;

        time = 0f;
        lerp = 0f;
    }

    private void UpdateFadeOut()
    {
        time += Time.deltaTime;
        lerp = Mathf.InverseLerp(0f, fadeOutTime, time);
        lerp = Easing.QuadEaseOut(lerp);
        lerp = 1f - lerp;

        if (lerp >= 1f){
            state = States.None;
        }

        UpdateVFX();
    }

    private void UpdateVFX(){
        whiteScreen.alpha = lerp;
        postProcessingController.SetBlizzardInfluenctMultiplier(lerp);
    }
}
