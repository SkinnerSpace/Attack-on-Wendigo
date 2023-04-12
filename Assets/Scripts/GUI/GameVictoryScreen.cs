using UnityEngine;

public class GameVictoryScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        GameEvents.current.onWendigosAreDefeated += Show;
    }

    private void Show()
    {
        canvasGroup.alpha = 1f;
    }
}
