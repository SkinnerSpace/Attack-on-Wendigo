public class PickUpTriggers
{
    private EventTrigger targetPresenceTrigger;
    private EventTrigger targetAbsenceTrigger;

    public void SetUp(EventManager events)
    {
        targetPresenceTrigger = new EventTrigger();
        events.ConnectTrigger(targetPresenceTrigger, "TargetIsPresent");

        targetAbsenceTrigger = new EventTrigger();
        events.ConnectTrigger(targetAbsenceTrigger, "TargetIsAbsent");
    }

    public void Activate(bool targetExist)
    {
        if (targetExist)
        {
            targetPresenceTrigger.SetActive(true);
            targetAbsenceTrigger.SetActive(false);
        }
        else
        {
            targetPresenceTrigger.SetActive(false);
            targetAbsenceTrigger.SetActive(true);
        }
    }
}
