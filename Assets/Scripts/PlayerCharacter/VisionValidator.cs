using System;
using System.Collections.Generic;
using UnityEngine;

public class VisionValidator : IVisionValidator
{
    private List<VisionTarget> samples = new List<VisionTarget>();

    public void AddSample(Type type, float distance) => samples.Add(new VisionTarget() { type = type, distance = distance });

    public VisionTarget Validate(VisionTarget target)
    {
        if (target.IsValid)
        {
            foreach (VisionTarget sample in samples)
                return Define(target, sample);
        }

        target.IsValid = false;
        return target;
    }

    private VisionTarget Define(VisionTarget target, VisionTarget sample)
    {
        VisionTarget definedTarget = target;

        bool suitableType = (target.type != null) ? (target.type == sample.type) : (target.Transform.GetComponent(sample.type) != null);
        bool suitableDistance = target.distance <= sample.distance;

        if (suitableType) definedTarget.type = sample.type;
        definedTarget.IsValid = suitableType && suitableDistance;

        return definedTarget;
    }
}
