﻿public abstract class ProtoTrigger
{
    public bool IsActive { get; protected set; }

    public abstract void AddObserver(ITriggerObserver observer);

    public virtual void Set(bool active)
    {
        if (IsActive != active)
            Notify();

        IsActive = active;
    }

    protected abstract void Notify();
}




