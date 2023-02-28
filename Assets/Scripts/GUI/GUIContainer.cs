using UnityEngine;

public class GUIContainer : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private EventManager eventManager;

    private EventListener gameOnStartListener;
    private EventListener gameOnPauseListener;
    private EventListener gameOnResumeListener;

    private void Awake()
    {
        gameOnStartListener = new EventListener(Show);
        eventManager.Subscribe(gameOnStartListener, "OnGameStart");

        gameOnPauseListener = new EventListener(Hid);
        eventManager.Subscribe(gameOnPauseListener, "OnGamePause");

        gameOnResumeListener = new EventListener(Show);
        eventManager.Subscribe(gameOnResumeListener, "OnGameResume");
    }

    public void Hid()
    {
        canvasGroup.alpha = 0f;
    }
    public void Show() => canvasGroup.alpha = 1f;
}
