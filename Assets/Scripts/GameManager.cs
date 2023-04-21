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
    [SerializeField] private InvasionCounter counter;
    [SerializeField] private Transform airdropImp;
    [SerializeField] private MenuSFXPlayer menuSFXPlayer;
    [SerializeField] private LevelLoader levelLoader;

    [SerializeField] private EventManager eventManager;
    private TriggersManager triggers;

    [SerializeField] private FunctionTimer timer;
    public static GameManager Instance;

    public Transform Character => character;

    public IMenu Menu => menu;
    public ISwitchable PlayerCharacter { get; private set; }
    public CameraManager CameraManager => cameraManager;
    public IAirdrop Airdrop { get; private set; }
    public TriggersManager Triggers => triggers;
    public LevelLoader LevelLoader => levelLoader;

    private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

    private bool isPlaying;

    private void Awake()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        PlayerCharacter = character.GetComponent<ISwitchable>();
        Airdrop = airdropImp.GetComponent<IAirdrop>();

        Instance = this;
        triggers = new TriggersManager(eventManager);
        
        commands.Add("Start", new GameStartCommand(this));
        commands.Add("Pause", new GamePauseCommand(this));
        commands.Add("Resume", new GameResumeCommand(this));
        commands.Add("Restart", new GameRestartCommand(this));
    }

    private void Start()
    {
        menu.Open("Main");
        cameraManager.TrackTheHelicopter();

        MenuEvents.current.onStart += commands["Start"].Execute;
        MenuEvents.current.onStart += OnPlay;

        MenuEvents.current.onResume += commands["Resume"].Execute;
        MenuEvents.current.onRestart += commands["Restart"].Execute;
    }

    private void Update()
    {
        if (isPlaying)
        {
            switch (GameState.PauseMode)
            {
                case PauseMode.None:
                    if (Input.GetKeyDown(KeyCode.Escape))
                        ExecuteCommand("Pause");

                    break;

                case PauseMode.Paused:
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        ExecuteCommand("Resume");
                        menuSFXPlayer.PlayMenuClose();
                    }

                    break;
            }
        }
    }

    public void ExecuteCommand(string command) => commands[command].Execute();

    public void OpenSettings()
    {
        Debug.Log("Settings");
    }

    public void QuitTheGame()
    {
        Debug.Log("QUIT");
    }

    private void OnPlay(){
        isPlaying = true;
    }
}
