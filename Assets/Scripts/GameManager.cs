using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Transform character;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Transform shakeManager;
    [SerializeField] private MenuManager menu;
    [SerializeField] private Transform airdropImp;
    [SerializeField] private MenuSFXPlayer menuSFXPlayer;
    [SerializeField] private FunctionTimer timer;
    public static GameManager Instance;

    public Transform Character => character;

    public IMenu Menu => menu;
    public ISwitchable PlayerCharacter { get; private set; }
    public CameraManager CameraManager => cameraManager;
    public IAirdrop Airdrop { get; private set; }

    private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

    private bool isPlaying;

    private void Awake()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        PlayerCharacter = character.GetComponent<ISwitchable>();
        Airdrop = airdropImp.GetComponent<IAirdrop>();

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
        cameraManager.TrackTheHelicopter();

        MenuEvents.current.onStart += commands["Start"].Execute;
        MenuEvents.current.onStart += OnPlay;

        MenuEvents.current.onResume += commands["Resume"].Execute;
        MenuEvents.current.onRestart += commands["Restart"].Execute;
        HelicopterEvents.current.onFlewAway += commands["Finish"].Execute;
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
}
