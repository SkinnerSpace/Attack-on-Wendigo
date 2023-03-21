using UnityEngine;
using System;

[Serializable]
public class BezierArrangement
{
    public Vector3 Pivot => pivot.position;
    [SerializeField] private Transform pivot;

    [Header("Line Length")]
    public float lineMinLength;
    public float lineMaxLength;

    [Header("Offset Beginning")]
    public float straightMinOffset;
    public float straightMaxOffset;

    [Header("Offset Length")]
    public float perpendicularMinOffset;
    public float perpendicularMaxOffset;

    [Header("Height")]
    public float minHeight;
    public float maxHeight;
}
