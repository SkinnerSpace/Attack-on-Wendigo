using UnityEngine;
using System;

[Serializable]
public class BezierArrangement
{
    public Vector3 Pivot => pivot.position;
    [SerializeField] private Transform pivot;

    public float minOffset;
    public float maxOffset;
    public float minHeight;
    public float maxHeight;
}
