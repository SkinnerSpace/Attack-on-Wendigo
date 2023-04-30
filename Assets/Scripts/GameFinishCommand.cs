using UnityEngine;

public class GameFinishCommand : ICommand
{
    public void Execute()
    {
        GameEvents.current.NotifyOnRestart();
        AudioEvent.StopAll();
        SceneLoader.Instance.FinishTheGame(0);
    }
}