using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Transform character;
    [SerializeField] private MenuManager menu;

    public static GameManager Instance;

    public Transform Character => character;

    public IMenu Menu => menu;
    public ISwitchable PlayerCharacter { get; private set; }

    private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

    private bool isPlaying;

    private void Awake()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        PlayerCharacter = character.GetComponent<ISwitchable>();
        Instance = this;
        
        commands.Add("Start", new GameStartCommand(this));
        commands.Add("Pause", new GamePauseCommand(this));
        commands.Add("Resume", new GameResumeCommand(this));
        commands.Add("Restart", new GameRestartCommand());
        commands.Add("Finish", new GameFinishCommand());
    }

    private void Start()
    {
        menu.OpenSilently("main");

        MenuEvents.current.onStart += commands["Start"].Execute;
        MenuEvents.current.onStart += OnPlay;

        MenuEvents.current.onResume += commands["Resume"].Execute;
        MenuEvents.current.onRestart += commands["Restart"].Execute;
        HelicopterEvents.current.onFlewAway += commands["Finish"].Execute;

        PlayerEvents.current.onDeath += StopPlaying;
    }

    private void Update()
    {
        if (isPlaying)
        {
            switch (GameState.PauseMode)
            {
                case PauseMode.None:
                    if (Input.GetKeyDown(KeyCode.Escape)){
                        ExecuteCommand("Pause");
                    }

                    break;

                case PauseMode.Paused:
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        ExecuteCommand("Resume");
                    }

                    break;
            }
        }
    }

    public void ExecuteCommand(string command) => commands[command].Execute();

    private void OnPlay(){
        isPlaying = true;
    }

    private void StopPlaying(){
        isPlaying = false;
    }
}
