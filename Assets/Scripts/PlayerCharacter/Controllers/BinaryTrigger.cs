public class BinaryTrigger 
{
    private EventTrigger positiveTrigger;
    private EventTrigger negativeTrigger;

    public BinaryTrigger(EventManager eventsManager, string positiveName, string negativeName)
    {
        positiveTrigger = new EventTrigger();
        eventsManager.ConnectTrigger(positiveTrigger, positiveName);

        negativeTrigger = new EventTrigger();
        eventsManager.ConnectTrigger(negativeTrigger, negativeName);
    }

    public void Trigger(bool positive)
    {
        if (positive)
        {
            positiveTrigger.SetActive(true);
            negativeTrigger.SetActive(false);
        }
        else if (!positive)
        {
            positiveTrigger.SetActive(false);
            negativeTrigger.SetActive(true);
        }
    }
}
