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
        FadeInBlackScreen,
        FadeInText,
        FadeOutText,
        FadeOutBlackScreen
    }

    private States state;

    [Header("Canvas groups")]
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private CanvasGroup text;

    [Header("Required Components")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image barFill;
    [SerializeField] private FunctionTimer timer;

    [Header("Fade Time")]
    [SerializeField] private float fadeInBlackScreenTime = 0.2f;
    [SerializeField] private float fadeInTextTime = 0.5f;
    [SerializeField] private float fadeOutTextTime = 0.2f;
    [SerializeField] private float fadeOutBlackScreenTime = 0.5f;

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
        StartToFadeInBlackScreen();
    }


    private void Update()
    {
        switch (state)
        {
            case States.FadeInBlackScreen:
                UpdateFadeInBlackScreen(); break;

            case States.FadeInText:
                UpdateFadeInText(); break;

            case States.FadeOutText:
                UpdateFadeOutText(); break;

            case States.FadeOutBlackScreen:
                UpdateFadeOutBlackScreen(); break;
        }
    }

    private void StartToFadeInBlackScreen(){
        time = 0f;
        state = States.FadeInBlackScreen;
        loadingScreen.SetActive(true);
    }

    private void UpdateFadeInBlackScreen()
    {
        Tick();
        transparency = Mathf.InverseLerp(0f, fadeInBlackScreenTime, time);
        blackScreen.alpha = transparency;

        if (transparency >= 1f){
            StartToFadeInText();
        }
    }

    private void StartToFadeInText(){
        time = 0f;
        state = States.FadeInText;
    }

    private void UpdateFadeInText()
    {
        Tick();
        transparency = Mathf.InverseLerp(0f, fadeInTextTime, time);
        text.alpha = transparency;

        if (transparency >= 1f){
            OnFadedIn();
        }
    }

    private void OnFadedIn(){
        state = States.None;
        StartCoroutine(LoadSceneAsync());
        GameState.PauseMode = PauseMode.None;
        AudioPlayer.StopAll();
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            //barFill.fillAmount = progressValue;

            yield return null;
        }

        Instance = this;
        
        StartToFadeOutText();
    }

    private void StartToFadeOutText(){
        state = States.FadeOutText;
        time = 0f;
    }

    private void UpdateFadeOutText()
    {
        Tick();
        transparency = 1f - Mathf.InverseLerp(0f, fadeOutTextTime, time);
        text.alpha = transparency;

        if (transparency <= Mathf.Epsilon){
            StartToFadeOutBlackScreen();
        }
    }

    private void StartToFadeOutBlackScreen(){
        state = States.FadeOutBlackScreen;
        time = 0f;
    }

    private void UpdateFadeOutBlackScreen()
    {
        Tick();
        transparency = 1f - Mathf.InverseLerp(0f, fadeOutBlackScreenTime, time);
        blackScreen.alpha = transparency;

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
