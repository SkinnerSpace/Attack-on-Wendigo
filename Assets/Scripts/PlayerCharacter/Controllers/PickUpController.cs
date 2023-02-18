using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PickUpController : BaseController, IVisionTriggerObserver, IVisionObserver
{
    private MainController main;
    private IVisionEvaluator evaluator;
    private VisionTarget target;
    private EventTrigger targetStatusTrigget;

    private bool hasTarget;

    public override void Initialize(MainController main)
    {
        this.main = main;

        evaluator = new VisionEvaluator();
        evaluator.AddSample(typeof(IPickable), 3f);

        main.GetController<EventsController>().ConnectTrigger(targetStatusTrigget);
    }

    public override void Connect() => main.GetController<VisionDetector>().AddObserver(this);

    public void OnTargetUpdate(VisionTarget target)
    {
        bool isSuitable = evaluator.IsSuitable(target);
        if (!isSuitable) target.IsValid = false;

        if (hasTarget != target.IsValid)
        {
            hasTarget = isSuitable;
            targetStatusTrigget.Set(hasTarget);
        }
    }
}

public class EventsController : BaseController
{
    private Dictionary<string, EventReceiver> events = new Dictionary<string, EventReceiver>();

    public override void Initialize(MainController main)
    {
        throw new NotImplementedException();
    }

    public override void Connect()
    {
        throw new NotImplementedException();
    }

    public void ConnectTrigger(EventTrigger eventTrigger, string eventName)
    {
        events[eventName].SubscribeOn(eventTrigger);

        // MAKE EVENET TRIGGER SYSTEM!!!
    }
}
