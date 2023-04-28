public class GameStartCommand : ICommand
{
    private GameManager gameManager;

    public GameStartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        gameManager.Menu.Close();

        GameState.PauseMode = PauseMode.None;
        AudioPlayer.ResumeAll();

        GameEvents.current.TheGameHasStarted();
    }
}
