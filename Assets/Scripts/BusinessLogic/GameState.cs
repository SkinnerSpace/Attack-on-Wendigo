using UnityEngine;

public static class GameState
{
    public static PauseMode PauseMode
    {
        get { return pauseMode; }
        set{
            pauseMode = value;
            Time.timeScale = (pauseMode == PauseMode.Paused) ? 0f : 1f;
        }
    }

    private static PauseMode pauseMode;
}
