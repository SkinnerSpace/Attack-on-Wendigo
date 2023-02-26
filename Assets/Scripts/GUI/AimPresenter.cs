using System.Collections.Generic;
using UnityEngine;

public class AimPresenter : MonoBehaviour
{
    [SerializeField] private Aim aim;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private MainController player;
    [SerializeField] private UGUIElement uGUIElement;

    private EventListener targetPresenceListener;
    private EventListener targetAbsenceListener;

    private EventListener gameOnStartListener;
    private EventListener gameOnPauseListener;
    private EventListener gameOnResumeListener;

    private List<Transform> targets = new List<Transform>();

    private void Awake()
    {
        /*player.GetController<PickUpController>().Subscribe(AddTarget, RemoveTarget);*/

        uGUIElement.Subscribe(OnTargetUpdate);

        gameOnStartListener = new EventListener(aim.Show);
        eventManager.Subscribe(gameOnStartListener, "OnGameStart");

        gameOnPauseListener = new EventListener(aim.Hid);
        eventManager.Subscribe(gameOnPauseListener, "OnGamePause");

        gameOnResumeListener = new EventListener(aim.Show);
        eventManager.Subscribe(gameOnResumeListener, "OnGameResume");
    }

    public void AddTarget(Transform target)
    {
        targets.Add(target);
        UpdateAimTargetState();
    }
    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
        UpdateAimTargetState();
    }

    public void OnTargetUpdate(bool targetExist)
    {
        if (targetExist)
        {
            aim.SetOnTarget();
        }
        else
        {
            aim.SetOffTarget();
        }
    }

    private void UpdateAimTargetState()
    {
        if (targets.Count > 0)
        {
            aim.SetOnTarget();
        }
        else
        {
            aim.SetOffTarget();
        }
    }
}
