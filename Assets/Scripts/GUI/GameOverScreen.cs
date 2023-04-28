using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        PlayerEvents.current.onDeath += Show;
    }

    private void Show()
    {
        canvasGroup.alpha = 1f;
    }
}
