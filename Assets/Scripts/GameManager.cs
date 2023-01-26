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

    [Header("Settings")]
    [SerializeField] private int wendigoMaxCount;
    [SerializeField] private float spawnInterval;

    private States state = States.Pause;
    private int wendigoCount = 0;

    private FunctionTimer timer;

    private void Awake()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        timer = GetComponent<FunctionTimer>();
    }

    private void Update()
    {
        StartTheGame();
    }

    private void StartTheGame()
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
