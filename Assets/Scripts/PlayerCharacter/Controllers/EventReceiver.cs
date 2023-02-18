using System.Collections.Generic;
using System;

public class EventReceiver : ProtoTrigger, ITriggerObserver
{
    private List<EventTrigger> triggers = new List<EventTrigger>();
    private event Action onUpdate;

    public override void AddObserver(ITriggerObserver observer) => onUpdate += observer.OnTriggerUpdate;
    public void SubscribeOn(EventTrigger trigger)
    {
        trigger.AddObserver(this);
        triggers.Add(trigger);
    }

    public void OnTriggerUpdate()
    {
        bool atLeastOneIsActive = false;

        foreach (EventTrigger trigger in triggers)
            if (trigger.IsActive) atLeastOneIsActive = true;

        Set(atLeastOneIsActive);
    }

    protected override void Notify() => onUpdate?.Invoke();
}




