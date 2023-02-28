using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private EventManager eventManager;

    private EventListener gameOverListener;

    private void Awake()
    {
        gameOverListener = new EventListener(Show);
        eventManager.Subscribe(gameOverListener, "PlayerDied");
    }

    private void Show()
    {
        canvasGroup.alpha = 1f;
    }
}