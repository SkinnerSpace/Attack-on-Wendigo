using UnityEngine;

public static class GlobalData
{
    public static bool showIntroOnStart = true;
    public static GameState initialGameState = GameState.Menu;

    public static PauseMode PauseMode
    {
        get { return pauseMode; }
        set{
            pauseMode = value;
            Time.timeScale = (pauseMode == PauseMode.Paused) ? 0f : 1f;
        }
    }

    private static PauseMode pauseMode;

    public static GameState gameState = GameState.Menu;
}
