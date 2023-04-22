using UnityEngine;

public class GamePauseCommand : ICommand
{
    private GameManager gameManager;

    public GamePauseCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        GameState.PauseMode = PauseMode.Paused;
        AudioPlayer.PauseAll();

        gameManager.Menu.Open("Pause");

        gameManager.Triggers.ResetTrigger("OnGameStart");
        gameManager.Triggers.ResetTrigger("OnGameResume");
        gameManager.Triggers.Trigger("OnGamePause");

        GameEvents.current.TheGameIsPaused();
    }
}
