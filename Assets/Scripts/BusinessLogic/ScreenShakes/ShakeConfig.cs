using System;
using UnityEngine;

[Serializable]
public class ShakeConfig
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
    [SerializeField] private float frequency;
    [SerializeField] private float attack;
    [SerializeField] private float release;

    private float power;

    public string Name => name;
    public Vector3 Axis => axis;
    public float Time => minTime + ((maxTime - minTime) * power);
    public float Strength => minStrength + ((maxStrength - minStrength) * power);
    public float AngleMultiplier => minAngleMult + ((maxAngleMult - minAngleMult) * power);
    public float Frenquency => frequency;
    public float Attack => attack;
    public float Release => release;

    public ShakeConfig SetPower(float power)
    {
        this.power = power;
        return this;
    }

    public void Launch(IShakeManager shakeManager)
    {
        ScreenShake.Create().withTime(Time).WithAxis(Axis.x, Axis.y, Axis.z).WithStrength(Strength, AngleMultiplier).WithCurve(Frenquency, Attack, Release).Launch(shakeManager);
    }
}
