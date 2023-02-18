using System;

public class EventTrigger : ProtoTrigger
{
    private event Action onUpdate;
    public override void AddObserver(ITriggerObserver observer) => onUpdate += observer.OnTriggerUpdate;
    protected override void Notify() => onUpdate?.Invoke();
}




