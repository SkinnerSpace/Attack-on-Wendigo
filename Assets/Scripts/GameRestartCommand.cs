public class GameRestartCommand : ICommand
{
    private GameManager gameManager;

    public GameRestartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        GameEvents.current.TheGameHasBeenRestarted();
        AudioEvent.StopAll();
        SceneLoader.Instance.LoadScene(0);
    }
}