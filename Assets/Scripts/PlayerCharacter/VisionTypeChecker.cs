using System;
using System.Collections.Generic;
using UnityEngine;

public class VisionTypeChecker : MonoBehaviour
{
    private List<Type> targetTypes = new List<Type>();

    private void Awake() => InitializeTargets();

    private void InitializeTargets()
    {
        targetTypes.Add(typeof(IOldPickable));
        targetTypes.Add(typeof(IDamageable));
    }

    public Type CheckType(Transform targetTransform)
    {
        foreach (Type type in targetTypes)
        {
            if (targetTransform.GetComponent(type) != null)
                return type;
        }

        return null;
    }
}
