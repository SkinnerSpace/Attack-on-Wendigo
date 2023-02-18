using System;

public class EventTrigger 
{
    public bool IsActive { get; protected set; }
    private event Action onUpdate;

    public void AddObserver(ITriggerObserver observer) => onUpdate += observer.OnTriggerUpdate;

    public virtual void SetActive(bool active)
    {
        bool wasActive = IsActive;
        IsActive = active;

        if (wasActive != IsActive)
            onUpdate?.Invoke();
    }
}




