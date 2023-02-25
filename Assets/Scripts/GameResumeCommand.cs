using UnityEngine;

public class GameResumeCommand : ICommand
{
    private GameManager gameManager;

    public GameResumeCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        gameManager.SetState(GameManager.States.Play);
        gameManager.Menu.Close();
        Time.timeScale = 1f;

        gameManager.Triggers.Trigger("OnGameResume");
    }
}