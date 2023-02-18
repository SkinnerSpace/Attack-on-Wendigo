using System;

public class EventListener : IEventListener
{
    private Action reactOnUpdate;

    public EventListener(Action reactOnUpdate) => this.reactOnUpdate = reactOnUpdate;
    public void OnUpdate() => reactOnUpdate();
}