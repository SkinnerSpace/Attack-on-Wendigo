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
    [SerializeField] private LoadingScreenText text;

    [Header("Required Components")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private FunctionTimer timer;

    [Header("Fade Time")]
    [SerializeField] private float defaultFadeInBlackScreenTime = 0.3f;
    [SerializeField] private float defaultFadeInTextTime = 0.2f;
    [SerializeField] private float fadeOutTextTime = 0.2f;
    [SerializeField] private float fadeOutBlackScreenTime = 0.3f;

    [Header("Fade In Finish Time")]
    [SerializeField] private float finishFadeInBlackScreenTime = 2f;
    [SerializeField] private float finishFadeInTextTime = 0.5f;

    public static SceneLoader Instance;

    private float time;
    private float transparency;

    private float fadeInBlackScreenTime;
    private float fadeInTextTime;

    private int sceneID;

    private void Awake(){
        Instance = this;
        SetDefaultFadeInTime();
    }

    public void RestartTheGame(int sceneID)
    {
        SetDefaultFadeInTime();
        text.SetLoadingText();
        LoadScene(sceneID);
    }

    public void FinishTheGame(int sceneID)
    {
        fadeInBlackScreenTime = finishFadeInBlackScreenTime;
        fadeInTextTime = finishFadeInTextTime;
        text.SetEndingText();
        LoadScene(sceneID);
    }

    private void SetDefaultFadeInTime()
    {
        fadeInBlackScreenTime = defaultFadeInBlackScreenTime;
        fadeInTextTime = defaultFadeInTextTime;
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
        text.SetAlpha(transparency);

        if (transparency >= 1f){
            OnFadedIn();
        }
    }

    private void OnFadedIn(){
        state = States.None;
        StartCoroutine(LoadSceneAsync());
        GlobalData.PauseMode = PauseMode.None;
        AudioPlayer.StopAll();
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone){
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
        text.SetAlpha(transparency);

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
