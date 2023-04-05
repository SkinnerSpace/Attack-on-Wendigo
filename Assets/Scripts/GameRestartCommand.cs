public class GameRestartCommand : ICommand
{
    private GameManager gameManager;

    public GameRestartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        AudioEvent.StopAll();
        PickUpManager.Instance.ResetState();
        gameManager.LevelLoader.ReloadScene();
    }
}