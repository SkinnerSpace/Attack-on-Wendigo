public class GameStartCommand : ICommand
{
    private GameManager gameManager;

    public GameStartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        gameManager.Menu.Close();
        gameManager.PlayerCharacter.SwitchOn();
        gameManager.CameraManager.TrackTheCharacter();
        gameManager.Airdrop.AddCargo(0);

        GameState.PauseMode = PauseMode.None;
        AudioPlayer.ResumeAll();

        gameManager.Triggers.Trigger("OnGameStart");
    }
}
