using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class InteractionController : BaseController, IVisionObserver, IVisionTrigger
{
    private MainController main;

    private float reachDistance = 3f;
    private Type suitable = typeof(IPickable);

    public bool IsTriggered { get; private set; }
    private bool wasTriggered;

    private event Action onTriggerUpdate;

    public override void Initialize(MainController main) => this.main = main;

    public override void Connect() => main.GetController<VisionController>().Subscribe(this);

    public void Subscribe(IVisionTrigger trigger) { }

    public void OnUpdate(VisionTarget target)
    {
        if (target.IsValid && target.distance <= reachDistance && target.Transform.GetComponent(suitable) != null)
        {
            IsTriggered = true;
        }

        if (IsTriggered != wasTriggered) onTriggerUpdate();

        wasTriggered = IsTriggered;
    }
}

// Make a trigger with multiple entries. If at leas one entry is active then trigger's gonna be active 

public class VisionTriggerAccumulator : IVisionTrigger
{
    private List<IVisionTrigger> triggers = new List<IVisionTrigger>();
    public bool WasTriggered { get; set; }
    public bool IsTriggered { get; set; }
    public Action notifyOnUpdate;

    public void AddTrigger(IVisionTrigger trigger) => triggers.Add(trigger);

    public void OnTriggerUpdate()
    {
        /*foreach (IVisionTrigger trigger in triggers)
        {
            if (trigger.IsTriggered)
            {
                IsTriggered = true;
                return;
            } 
        }

        IsTriggered = false;*/
    }
}

public interface IVisionTrigger
{

}
