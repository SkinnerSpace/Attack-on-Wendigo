public class GameRestartCommand : ICommand
{
    private GameManager gameManager;

    public GameRestartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        AudioEvent.StopAll();
        gameManager.LevelLoader.ReloadScene();
    }
}