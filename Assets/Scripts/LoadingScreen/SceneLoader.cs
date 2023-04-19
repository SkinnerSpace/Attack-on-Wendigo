using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private enum States
    {
        None,
        FadeIn,
        FadeOut
    }

    private States state;

    [Header("Required Components")]
    [SerializeField] private CanvasGroup group;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image barFill;

    [Header("Fade Time")]
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeOutTime = 0.5f;

    public static SceneLoader Instance;

    private float time;
    private float transparency;

    private int sceneID;

    private void Awake(){
        Instance = this;
    }

    public void LoadScene(int sceneID)
    {
        this.sceneID = sceneID;
        StartToFadeIn();
    }

    private void StartToFadeIn(){
        time = 0f;
        state = States.FadeIn;
        loadingScreen.SetActive(true);
    }

    private void Update()
    {
        switch (state)
        {
            case States.FadeIn:
                FadingIn(); break;

            case States.FadeOut:
                FadingOut(); break;
        }
    }

    private void FadingIn()
    {
        Tick();
        transparency = Mathf.InverseLerp(0f, fadeInTime, time);
        group.alpha = transparency;

        if (transparency >= 1f){
            OnFadedIn();
        }
    }

    private void OnFadedIn(){
        state = States.None;
        StartCoroutine(LoadSceneAsync());
        GameState.PauseMode = PauseMode.None;
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            barFill.fillAmount = progressValue;

            yield return null;
        }

        AudioPlayer.ResumeAll();
        StartToFadeOut();
    }

    private void StartToFadeOut(){
        time = 0f;
        state = States.FadeOut;
    }

    private void FadingOut()
    {
        Tick();
        transparency = 1f - Mathf.InverseLerp(0f, fadeOutTime, time);
        group.alpha = transparency;

        if (transparency <= Mathf.Epsilon){
            OnFadedOut();
        }
    }

    private void OnFadedOut(){
        state = States.None;
        loadingScreen.SetActive(false);
    }

    private void Tick()
    {
        if (Time.unscaledDeltaTime < 1f){
            time += Time.unscaledDeltaTime;
        }
    }
}
