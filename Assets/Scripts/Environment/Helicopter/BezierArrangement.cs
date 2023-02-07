using UnityEngine;
using System;

[Serializable]
public class BezierArrangement
{
    public Vector3 Pivot => pivot.position;
    [SerializeField] private Transform pivot;

    public float lineMinLength;
    public float lineMaxLength;

    public float straightMinOffset;
    public float straightMaxOffset;

    public float perpendicularMinOffset;
    public float perpendicularMaxOffset;
}
