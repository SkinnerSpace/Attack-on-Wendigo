using System.Collections.Generic;
using System;

public class VisionTrigger
{
    private List<VisionTrigger> triggers = new List<VisionTrigger>();
    public bool IsActive { get; private set; }
    private bool wasActive;

    Action notifyOnUpdate;

    public void Subscribe(VisionTrigger trigger)
    {
        notifyOnUpdate += trigger.OnUpdate;
        trigger.Add(this);
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
        notifyOnUpdate?.Invoke();
    }

    public void Add(VisionTrigger trigger) => triggers.Add(trigger);

    public void OnUpdate()
    {
        bool atLeastOneIsActive = false;

        foreach (VisionTrigger trigger in triggers)
            if (trigger.IsActive) atLeastOneIsActive = true;

        IsActive = atLeastOneIsActive;
        if (wasActive != IsActive) notifyOnUpdate?.Invoke();

        wasActive = IsActive;
    }
}
