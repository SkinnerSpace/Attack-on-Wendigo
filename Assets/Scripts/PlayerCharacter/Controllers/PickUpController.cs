using System.Collections;
using System;

public class PickUpController : BaseController, IVisionTriggerObserver, IVisionObserver
{
    private MainController main;
    private IVisionEvaluator evaluator;
    private VisionTarget target;

    private EventTrigger targetPresenceTrigger;
    private EventTrigger targetAbsenceTrigger;

    private bool hasTarget;

    public override void Initialize(MainController main)
    {
        this.main = main;

        evaluator = new VisionEvaluator();
        evaluator.AddSample(typeof(IPickable), 3f);

        targetPresenceTrigger = new EventTrigger();
        main.Events.ConnectTrigger(targetPresenceTrigger, "TargetIsPresent");

        targetAbsenceTrigger = new EventTrigger();
        main.Events.ConnectTrigger(targetAbsenceTrigger, "TargetIsAbsent");
    }

    public override void Connect() => main.GetController<VisionDetector>().AddObserver(this);

    public void OnTargetUpdate(VisionTarget target)
    {
        bool isSuitable = evaluator.IsSuitable(target);
        if (!isSuitable) target.IsValid = false;

        if (hasTarget != target.IsValid)
        {
            hasTarget = isSuitable;

            if (hasTarget)
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
}
