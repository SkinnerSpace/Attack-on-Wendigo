using UnityEngine;
using System;

[Serializable]
public class ShakeSettings
{
    [Header("Time")]
    public float minTime;
    public float maxTime;

    [Header("Strength")]
    public float strength;
    public float angleMultiplier;

    [Header("Curve")]
    public float frequency;
    public float attack;
    public float release;

    [Header("Attenuation")]
    public float maxDistance;
    public float maxVelocity;
}