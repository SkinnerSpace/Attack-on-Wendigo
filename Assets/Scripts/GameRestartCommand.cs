using UnityEngine;

public class GameRestartCommand : ICommand
{
    public void Execute()
    {
        GameEvents.current.NotifyOnRestart();
        AudioEvent.StopAll();
        SceneLoader.Instance.RestartTheGame(0);
    }
}
