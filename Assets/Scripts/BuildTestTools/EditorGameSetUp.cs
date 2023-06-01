using UnityEngine;

public class EditorGameSetUp : MonoBehaviour
{
    [Header("Settings")]
    public bool skipTheIntroOnStart;
    public GameState initialGameState;

    private void Awake()
    {
        if (skipTheIntroOnStart)
        {
            GlobalData.showIntroOnStart = false;
        }

        GlobalData.initialGameState = initialGameState;
    }
}