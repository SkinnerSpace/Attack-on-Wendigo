public class GameRestartCommand : ICommand
{
    private GameManager gameManager;

    public GameRestartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        AudioEvent.StopAll();
        GameEvents.current.ResetState();

        SceneLoader.Instance.LoadScene(0);
        /*GameState.PauseMode = PauseMode.None;
        AudioPlayer.ResumeAll();*/

        //gameManager.LevelLoader.ReloadScene();
    }
}