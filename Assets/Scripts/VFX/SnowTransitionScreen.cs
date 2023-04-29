using System;
using UnityEngine;

public class SnowTransitionScreen : MonoBehaviour
{
    private enum States
    {
        None,
        FadeIn,
        FadeOut
    }

    [Header("Required Components")]
    [SerializeField] private CanvasGroup whiteScreen;
    [SerializeField] private ParticleSystem snowParticles;
    [SerializeField] private BlizzardPostProcessingController postProcessingController;
    [SerializeField] private FunctionTimer timer;

    [Header("Time")]
    [SerializeField] private float fadeInTime = 2f;
    [SerializeField] private float delayTime = 0.1f;
    [SerializeField] private float fadeOutTime = 2f;

    private States state;

    private float time;
    private float opacity;

    private SnowScreenSFXPlayer sFXPlayer;
    private Action executeOnTransition;

    private void Awake(){
        sFXPlayer = GetComponent<SnowScreenSFXPlayer>();
    }

    public void SetOnTransitionAction(Action executeOnTransition){
        this.executeOnTransition = executeOnTransition;
        FadeIn();
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
        time = 0f;
        opacity = 0f;

        state = States.FadeIn;
        snowParticles.Play();
        sFXPlayer.Play();
    }

    private void UpdateFadeIn()
    {
        time += Time.deltaTime;
        opacity = Mathf.InverseLerp(0f, fadeInTime, time);
        opacity = Easing.QuadEaseIn(opacity);

        if (opacity >= 1f){
            OnFadedIn();
        }

        UpdateVFX();
    }

    private void OnFadedIn()
    {
        state = States.None;
        timer.Set("FadeOut", delayTime, FadeOut);
        executeOnTransition();
    }

    private void FadeOut()
    {
        snowParticles.Stop();
        state = States.FadeOut;

        time = 0f;
        opacity = 0f;
    }

    private void UpdateFadeOut()
    {
        time += Time.deltaTime;
        opacity = Mathf.InverseLerp(0f, fadeOutTime, time);
        opacity = Easing.QuadEaseOut(opacity);
        opacity = 1f - opacity;

        if (opacity >= 1f){
            OnFadedOut();
        }

        UpdateVFX();
    }

    private void OnFadedOut(){
        state = States.None;
    }

    private void UpdateVFX(){
        whiteScreen.alpha = opacity;
        postProcessingController.SetAdditionalInfluence(opacity);
    }
}
