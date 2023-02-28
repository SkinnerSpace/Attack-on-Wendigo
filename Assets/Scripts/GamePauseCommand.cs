using UnityEngine;

public class GamePauseCommand : ICommand
{
    private GameManager gameManager;

    public GamePauseCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        gameManager.SetState(GameManager.States.Pause);
        gameManager.Menu.Open("Pause");
        Time.timeScale = 0f;

        gameManager.Triggers.ResetTrigger("OnGameStart");
        gameManager.Triggers.ResetTrigger("OnGameResume");
        gameManager.Triggers.Trigger("OnGamePause");
    }
}
