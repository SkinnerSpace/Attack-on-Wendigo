using System.Collections.Generic;
using System;

public class VisionTrigger : IVisionObserver
{
    private List<VisionTrigger> triggers = new List<VisionTrigger>();
    private IVisionValidator evaluator;

    public bool IsActive { get; private set; }
    private bool wasActive;

    Action notifyDependee;
    Action<VisionTarget> notifyObservers;

    public void SetEvaluator(IVisionValidator evaluator) => this.evaluator = evaluator;

    public void AddDependee(VisionTrigger dependee)
    {
        notifyDependee += dependee.OnTriggerUpdate;
        dependee.SetDependenceOn(this);
    }

    public void SetDependenceOn(VisionTrigger trigger) => triggers.Add(trigger);

    public void AddObserver(IVisionTriggerObserver observer) => notifyObservers += observer.OnTargetUpdate; 

    public void OnTriggerUpdate()
    {
        bool atLeastOneIsActive = false;

        foreach (VisionTrigger trigger in triggers)
            if (trigger.IsActive) atLeastOneIsActive = true;

        if (StateHasChanged(atLeastOneIsActive))
            notifyDependee?.Invoke();
    }

    public void OnTargetUpdate(VisionTarget target)
    {
        bool targetIsSuitable = false;//evaluator.Validate(target);

        if (!targetIsSuitable) 
            target.IsValid = false;

        if (StateHasChanged(target.IsValid))
        {
            notifyDependee?.Invoke();
            notifyObservers?.Invoke(target);
        }
    }

    public void SetActive(bool isActive)
    {
        if (StateHasChanged(isActive)) 
            notifyDependee?.Invoke();
    }

    private bool StateHasChanged(bool currentState)
    {
        IsActive = currentState;
        bool hasChanged = wasActive != IsActive;
        wasActive = IsActive;

        return hasChanged;
    }
}




