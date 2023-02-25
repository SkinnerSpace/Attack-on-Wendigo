using UnityEngine;

public class AimPresenter : MonoBehaviour
{
    [SerializeField] private Aim aim;
    [SerializeField] private EventManager eventManager;

    private EventListener targetPresenceListener;
    private EventListener targetAbsenceListener;

    private EventListener gameOnStartListener;
    private EventListener gameOnPauseListener;
    private EventListener gameOnResumeListener;

    private void Awake()
    {
        targetPresenceListener = new EventListener(aim.SetOnTarget);
        eventManager.Subscribe(targetPresenceListener, "TargetIsPresent");

        targetAbsenceListener = new EventListener(aim.SetOffTarget);
        eventManager.Subscribe(targetAbsenceListener, "TargetIsAbsent");

        gameOnStartListener = new EventListener(aim.Show);
        eventManager.Subscribe(gameOnStartListener, "OnGameStart");

        gameOnPauseListener = new EventListener(aim.Hid);
        eventManager.Subscribe(gameOnPauseListener, "OnGamePause");

        gameOnResumeListener = new EventListener(aim.Show);
        eventManager.Subscribe(gameOnResumeListener, "OnGameResume");
    }
}
