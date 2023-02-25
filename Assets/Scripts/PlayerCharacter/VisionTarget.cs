using System;
using UnityEngine;

public class VisionTarget
{
    public bool IsValid { get; set; }
    public Transform Transform { get; set; }

    public Type type;
    public float distance;
}

public class NullVisionTarget : VisionTarget
{
    public static NullVisionTarget Instance
    {
        get
        {
            if (instance == null)
                instance = new NullVisionTarget();

            return instance;
        }
    }

    private static NullVisionTarget instance;

    private NullVisionTarget() { }
}