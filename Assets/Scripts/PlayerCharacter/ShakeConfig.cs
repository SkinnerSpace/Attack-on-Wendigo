﻿using System;
using UnityEngine;

[Serializable]
public class ShakeConfig : IShakeConfig
{
    [SerializeField] private string name;
    [SerializeField] private Vector3 axis;

    [Header("Time")]
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;

    [Header("Strength")]
    [SerializeField] private float minStrength;
    [SerializeField] private float maxStrength;

    [Header("Angle")]
    [SerializeField] private float minAngleMult;
    [SerializeField] private float maxAngleMult;

    [Header("Curve")]
    [SerializeField] private float minFrequency;
    [SerializeField] private float maxFrequency;
    [SerializeField] private float attack;
    [SerializeField] private float release;

    private float power;

    public string Name => name;
    public Vector3 Axis => axis;
    public float Time => minTime + ((maxTime - minTime) * power);
    public float Strength => minStrength + ((maxStrength - minStrength) * power);
    public float AngleMultiplier => minAngleMult + ((maxAngleMult - minAngleMult) * power);
    public float Frenquency => minFrequency + ((maxFrequency - minFrequency) * power);
    public float Attack => attack;
    public float Release => release;

    public IShakeConfig SetPower(float power)
    {
        this.power = power;
        return this;
    }

    public void Launch(IShakeManager shakeManager)
    {
        ShakeBuilder.Create().
                        withTime(Time).
                        WithAxis(Axis.x, Axis.y, Axis.z).
                        WithStrength(Strength, AngleMultiplier).
                        WithCurve(Frenquency, Attack, Release).
                        BuildAndLaunch(shakeManager);
    }
}
