using UnityEngine;

public class GamePauseCommand : ICommand
{
    private GameManager gameManager;

    public GamePauseCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        GlobalData.PauseMode = PauseMode.Paused;
        AudioPlayer.PauseAll();

        gameManager.Menu.Open("pause");

        GameEvents.current.TheGameIsPaused();
    }
}
