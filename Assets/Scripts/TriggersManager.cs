using System.Collections.Generic;

public class TriggersManager 
{
    private EventManager eventManager;
    private Dictionary<string, EventTrigger> triggers;

    public TriggersManager(EventManager eventManager)
    {
        this.eventManager = eventManager;
        triggers = new Dictionary<string, EventTrigger>();
    }

    public void Trigger(string triggerName)
    {
        CreateIfNecessary(triggerName);
        triggers[triggerName].SetActive(true);
    }

    public void ResetTrigger(string triggerName)
    {
        CreateIfNecessary(triggerName);
        triggers[triggerName].SetActive(false);
    }

    private void CreateIfNecessary(string triggerName)
    {
        if (!triggers.ContainsKey(triggerName))
        {
            EventTrigger trigger = new EventTrigger();
            triggers.Add(triggerName, trigger);
            eventManager.ConnectTrigger(trigger, triggerName);
        }
    }
}
