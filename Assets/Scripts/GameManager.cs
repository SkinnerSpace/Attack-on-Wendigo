using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum States
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
    [SerializeField] private InvasionCounter counter;
    [SerializeField] private Airdrop airdrop;
    [SerializeField] private LevelLoader levelLoader;

    [SerializeField] private EventManager eventManager;
    private TriggersManager triggers;

    [SerializeField] private FunctionTimer timer;
    public static GameManager Instance;

    public MenuManager Menu => menu;
    public MainController Character => character;
    public CameraManager CameraManager => cameraManager;
    public Airdrop Airdrop => airdrop;
    public TriggersManager Triggers => triggers;
    public LevelLoader LevelLoader => levelLoader;

    private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

    private void Awake()
    {
        Instance = this;
        triggers = new TriggersManager(eventManager);
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        cameraManager.SetLookAtTheHelicopter();

        commands.Add("Start", new GameStartCommand(this));
        commands.Add("Pause", new GamePauseCommand(this));
        commands.Add("Resume", new GameResumeCommand(this));
        commands.Add("Restart", new GameRestartCommand(this));
    }

    private void Start()
    {
        menu.Open("Main");
    }

    private void Update()
    {
        switch (state)
        {
            case States.Play:
                if (Input.GetKeyDown(KeyCode.Escape))
                    ExecuteCommand("Pause");

                break;

            case States.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                    ExecuteCommand("Resume");

                break;
        }
    }

    public void SetState(States state) => this.state = state;

    public void ExecuteCommand(string command) => commands[command].Execute();

    public void OpenSettings()
    {
        Debug.Log("Settings");
    }

    public void QuitTheGame()
    {
        Debug.Log("QUIT");
    }
}
