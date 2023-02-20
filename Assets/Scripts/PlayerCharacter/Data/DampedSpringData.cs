using System;
using UnityEngine;

[Serializable]
public class DampedSpringData : IDampedSpringData
{
    [SerializeField] private float power = 1f;
    [SerializeField] private float time = 0.5f;

    public float Power => power;
    public float Time => time;
    public float Amplitude { get; set; }
    public float CurrentTime { get; set; }
}