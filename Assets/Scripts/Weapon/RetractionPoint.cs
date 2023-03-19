using System;
using UnityEngine;

[Serializable]
public class RetractionPoint
{
    [SerializeField] public Transform transform;
    [SerializeField] public Vector3 extendedPosition;
    [SerializeField] public Vector3 retractedPosition;
}