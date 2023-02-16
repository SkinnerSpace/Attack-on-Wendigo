using System;
using System.Collections.Generic;
using UnityEngine;

public class VisionEvaluator : MonoBehaviour, IVisionEvaluator
{
    [SerializeField] private List<VisionTarget> appropriateTargets = new List<VisionTarget>();

    public bool TargetIsSuitable(VisionTarget target)
    {
        if (target.IsValid)
        {
            foreach (VisionTarget appropriate in appropriateTargets)
                if (Suits(target, appropriate)) return true;
        }

        return false;
    }

    private bool Suits(VisionTarget target, VisionTarget appropriate)
    {
        return target.Transform.GetComponent(appropriate.type) != null && 
               target.distance <= appropriate.distance;
    }
}
