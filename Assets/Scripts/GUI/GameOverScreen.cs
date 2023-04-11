using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        GameEvents.current.onPlayerHasDied += Show;
    }

    private void Show()
    {
        canvasGroup.alpha = 1f;
    }
}
