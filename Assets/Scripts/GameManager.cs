using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum States
    {
        Pause,
        Play
    }

    [Header("Required Components")]
    [SerializeField] private WendigoSpawner wendigoSpawner;
    [SerializeField] private MainController character;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private MainMenu menu;

    [Header("Settings")]
    [SerializeField] private int wendigoMaxCount;
    [SerializeField] private float spawnInterval;

    private States state = States.Pause;
    private int wendigoCount = 0;

    private FunctionTimer timer;

    public static GameManager Instance;

    private event Action onStart;

    private void Awake()
    {
        Instance = this;

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        timer = GetComponent<FunctionTimer>();

        cameraManager.SetLookAtTheHelicopter();

    }

    private void Update()
    {
        StarInvasion();
    }

    public void StartTheGame()
    {
        CursorManager.LockCursor();
        menu.gameObject.SetActive(false);

        character.SetActive(true);
        cameraManager.SetLookAtTheCharacter();
    }

    public void OpenSettings()
    {
        Debug.Log("Settings");
    }

    public void QuitTheGame()
    {
        Debug.Log("QUIT");
    }

    private void StarInvasion()
    {
        if (state == States.Pause && Input.GetKeyDown(KeyCode.G))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        wendigoSpawner.Launch();
        wendigoCount += 1;

        if (wendigoCount < wendigoMaxCount)
        {
            timer.Set("Spawn", spawnInterval, Spawn);
        }
    }
}
