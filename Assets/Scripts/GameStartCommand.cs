public class GameStartCommand : ICommand
{
    private GameManager gameManager;

    public GameStartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        gameManager.SetState(GameManager.States.Play);
        gameManager.Menu.Close();
        gameManager.PlayerCharacter.SwitchOn();
        gameManager.CameraManager.TrackTheCharacter();
        gameManager.Airdrop.AddCargo(0);
        gameManager.Triggers.Trigger("OnGameStart");
    }
}
