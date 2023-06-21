using UnityEngine;

public class EditorGameSetUp : MonoBehaviour
{
    [Header("Settings")]
    public bool skipTheIntroOnStart;
    public GameState initialGameState;

    [SerializeField] private FunctionTimer timer;

    private void Awake()
    {
        if (skipTheIntroOnStart)
        {
            GlobalData.showIntroOnStart = false;
        }
        else
        {
            GlobalData.showIntroOnStart = true;
        }

        GlobalData.initialGameState = initialGameState;

        StartTheGameIfInTheGameplayState();
    }

    private void StartTheGameIfInTheGameplayState()
    {
        if (GlobalData.initialGameState == GameState.Gameplay)
        {
            timer.Set("StartTheGame", 1f, StartTheGame);
        }
    }

    private void StartTheGame()
    {
        //GameEvents.current.TheGameHasStarted();
        MenuEvents.current.StartTheGame();
    }
}