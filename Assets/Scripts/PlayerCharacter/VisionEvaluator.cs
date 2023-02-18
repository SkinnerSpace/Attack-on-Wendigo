using System;
using System.Collections.Generic;
using UnityEngine;

public class VisionEvaluator : IVisionEvaluator
{
    private List<VisionTarget> samples = new List<VisionTarget>();

    public void AddSample(Type type, float distance) => samples.Add(new VisionTarget() { type = type, distance = distance });

    public bool IsSuitable(VisionTarget target)
    {
        if (target.IsValid)
        {
            foreach (VisionTarget sample in samples)
                return Suits(target, sample);
        }

        return false;
    }

    private bool Suits(VisionTarget target, VisionTarget sample)
    {
        bool suitableType = (target.type != null) ? (target.type == sample.type) : (target.Transform.GetComponent(sample.type) != null);
        bool suitableDistance = target.distance <= sample.distance;

        return suitableType && suitableDistance;
    }
}
