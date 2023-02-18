using UnityEngine;

public class AimPresenter : MonoBehaviour
{
    [SerializeField] private Aim aim;
    [SerializeField] private EventManager eventManager;

    private EventListener targetPresenceListener;
    private EventListener targetAbsenceListener;

    private void Awake()
    {
        targetPresenceListener = new EventListener(aim.SetOnTarget);
        eventManager.Subscribe(targetPresenceListener, "TargetIsPresent");

        targetAbsenceListener = new EventListener(aim.SetOffTarget);
        eventManager.Subscribe(targetAbsenceListener, "TargetIsAbsent");
    }
}
