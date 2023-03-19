using UnityEngine;
using System.Collections.Generic;
using System;

public class GameEvent : ITriggerObserver
{
    private List<EventTrigger> triggers = new List<EventTrigger>();
    private event Action onUpdate;

    public bool IsActive { get; protected set; }

    public void SubscribeOnStart(IEventListener listener) => onUpdate += listener.OnUpdate;
    public void UnsubscribeOnStart(IEventListener listener) => onUpdate -= listener.OnUpdate;

    public void Connect(EventTrigger trigger)
    {
        trigger.AddObserver(this);
        triggers.Add(trigger);
    }

    public void OnTriggerUpdate()
    {
        bool atLeastOneIsActive = false;

        foreach (EventTrigger trigger in triggers)
            if (trigger.IsActive) atLeastOneIsActive = true;

        SetActive(atLeastOneIsActive);
    }

    public virtual void SetActive(bool active)
    {
        if (IsActive != active && active)
            onUpdate?.Invoke();

        IsActive = active;
    }
}




