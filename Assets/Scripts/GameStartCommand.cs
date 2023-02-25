public class GameStartCommand : ICommand
{
    private GameManager gameManager;

    public GameStartCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        gameManager.SetState(GameManager.States.Play);
        gameManager.Menu.Close();
        gameManager.Character.SetActive(true);
        gameManager.CameraManager.SetLookAtTheCharacter();
        gameManager.Airdrop.DropPistol();
        gameManager.Triggers.Trigger("OnGameStart");
    }
}
