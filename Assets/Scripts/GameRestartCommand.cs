public class GameRestartCommand : ICommand
{
    private GameManager gameManager;

    public GameRestartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        AudioEvent.StopAll();
        GameEvents.current.ResetState();
        gameManager.LevelLoader.ReloadScene();
    }
}