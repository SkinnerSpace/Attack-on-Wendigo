using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum States
    {
        Menu,
        Play,
        Pause
    }

    private States state = States.Menu;

    [Header("Required Components")]
    [SerializeField] private WendigoSpawner wendigoSpawner;
    [SerializeField] private MainController character;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private MenuManager menu;
    
    private FunctionTimer timer;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        timer = GetComponent<FunctionTimer>();

        cameraManager.SetLookAtTheHelicopter();
    }

    private void Start()
    {
        menu.OpenMenu("Main");
    }

    private void Update()
    {
        switch (state)
        {
            case States.Play:
                StarTheInvasion();

                if (Input.GetKeyDown(KeyCode.Escape))
                    PauseTheGame();

                break;

            case States.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                    UnpauseTheGame();

                break;
        }
    }

    public void StartTheGame()
    {
        state = States.Play;

        menu.CloseMenu();

        character.SetActive(true);
        cameraManager.SetLookAtTheCharacter();
    }

    public void RestartTheGame()
    {
        Debug.Log("Restart");
    }
/*
    public void ToMain()
    {
        state = States.Menu;

        CursorManager.UnlockCursor();
        menu.OpenMenu("Main");

        character.SetActive(false);
        Debug.Log("Back to main");
        //cameraManager.SetLookAtTheHelicopter();
    }*/

    private void StarTheInvasion()
    {
        if (Input.GetKeyDown(KeyCode.G))
            wendigoSpawner.Spawn();
    }

    private void PauseTheGame()
    {
        state = States.Pause;

        menu.OpenMenu("Pause");
        Time.timeScale = 0f;
    }

    public void UnpauseTheGame()
    {
        state = States.Play;

        menu.CloseMenu();
        Time.timeScale = 1f;
    }

    public void OpenSettings()
    {
        Debug.Log("Settings");
    }

    public void QuitTheGame()
    {
        Debug.Log("QUIT");
    }
}
