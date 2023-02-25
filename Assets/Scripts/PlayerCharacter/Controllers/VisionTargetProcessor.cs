using System;
using UnityEngine;

public class VisionTargetProcessor
{
    private IVisionValidator validator;
    private BinaryTrigger binaryTrigger;
    private Action<Transform> onTargetUpdate;

    public VisionTargetProcessor(IVisionValidator validator, BinaryTrigger binaryTrigger, Action<Transform> onTargetUpdate)
    {
        this.validator = validator;
        this.binaryTrigger = binaryTrigger;
        this.onTargetUpdate = onTargetUpdate;
    }

    public void ProcessTarget(VisionTarget currentTarget, VisionTarget newTarget)
    {
        VisionTarget testedTarget = validator.Validate(newTarget);
        
        if (TargetHasChanged(currentTarget, testedTarget))
        {
            currentTarget = testedTarget;
            binaryTrigger.Trigger(currentTarget.IsValid);

            Transform targetObject = (currentTarget.IsValid) ? currentTarget.Transform : null;
            onTargetUpdate?.Invoke(targetObject);
        }
    }

    private bool TargetHasChanged(VisionTarget currentTarget, VisionTarget newTarget) => currentTarget.IsValid != newTarget.IsValid || currentTarget.type != newTarget.type;
}