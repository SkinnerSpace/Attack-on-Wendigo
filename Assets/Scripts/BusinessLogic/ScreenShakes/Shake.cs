using UnityEngine;
using System;

public class Shake
{
    private const float WAVE_UNIT = Mathf.PI * 2f;
    private const float DEVIATION_MULTIPLIER = 0.1f;

    public bool isActive { get; private set; }
    public float Completeness { get; private set; }

    public Vector3 MaxPosDisplacement => data.Direction * strength.amount;
    public Vector3 MaxAngleDisplacement => data.Angle * strength.amount * strength.angleMultiplier;

    public Vector3 Dir => data.Direction;
    public Vector3 Angle => data.Angle;
    public Vector3 Axis => data.Axis;

    private ShakeData data;

    public float Attenuation
    {
        get{
            if (recipient != null)
                CalculateAttenuation();

            return attenuation;
        }
    }

    private float attenuation;

    private ShakeStrength strength;
    private ShakeCurve curve;

    private ShakeTimer timer;

    public float exWave { get; private set; }
    public float wave { get; private set; }

    private Transform recipient;
    private float maxDistance;
    private Vector3 position;

    public Shake()
    {
        data = new ShakeData();
    }

    public void SetTimer(ShakeTimer timer) => this.timer = timer;
    public void SetCurve(ShakeCurve curve) => this.curve = curve;
    public void SetStrength(ShakeStrength strength) => this.strength = strength;

    public Shake(Vector3 axis, ShakeStrength strength, ShakeCurve curve, float attenuation, ShakeTimer timer = null)
    {
        data = new ShakeData();

        data.Axis = axis;
        this.strength = strength;
        this.curve = curve;
        this.attenuation = attenuation;
        this.timer = timer;
    }

    public Shake(Vector3 axis, ShakeStrength strength, ShakeCurve curve, Transform recipient, Vector3 position, float maxDistance, ShakeTimer timer = null)
    {
        data = new ShakeData();

        data.Axis = axis;
        this.strength = strength;
        this.curve = curve;
        this.recipient = recipient;
        this.position = position;
        this.maxDistance = maxDistance;
        this.timer = timer;
    }

    private void CalculateAttenuation()
    {
        float distance = Vector3.Distance(recipient.position, position);
        attenuation = 1f - (distance / maxDistance);
        if (attenuation <= 0f) attenuation = 0f;
        else attenuation *= attenuation;
    }

    public void Launch()
    {
        if (!isActive)
        {
            data.Direction = RandomizeVector(data.Axis);
            data.Angle = GetRandAngle();
            isActive = true;
        }
    }

    public void Proceed()
    {
        timer.CountDown();
        Completeness = timer.GetCompleteness();

        isActive = Completeness < 1f;
        Update();
    }

    public void SetCompleteness(float completeness)
    {
        Completeness = completeness;
        isActive = completeness < 1f;
        Update();
    }

    public void Update()
    {
        exWave = wave;
        wave = GetRawWave();

        if (HasPassed())
            ModifyDirection();
    }

    public bool HasPassed() => (exWave < 0f) && (wave >= 0f);
    public void SetAxis(Vector3 axis) => data.Axis = axis;
    public void SetDir(Vector3 direction) => data.Direction = direction;
    public void SetAttenuation(float attenuation) => this.attenuation = attenuation;

    public void ModifyDirection()
    {
        Vector3 deviation = RandomizeVector(Axis) * DEVIATION_MULTIPLIER;
        Vector3 modifiedDir = (data.Direction + deviation).normalized;

        data.Direction = modifiedDir;
    }

    public Vector3 RandomizeVector(Vector3 axis)
    {
        Vector3 randomizedVector;

        randomizedVector = new Vector3(
                           x: Rand.Range(-1f, 1f) * axis.x,
                           y: Rand.Range(-1f, 1f) * axis.y,
                           z: Rand.Range(-1f, 1f) * axis.z).
                           normalized;

        return randomizedVector;
    }

    public Vector3 GetRandAngle()
    {
        float zAngle = (Rand.Range(0f, 1f) < 0.5f) ? -1 : 1f;
        Vector3 randomAngle = new Vector3(0f, 0f, zAngle);
        return randomAngle;
    }

    public ShakeDisplacement GetDisplacement()
    {
        float wave = GetWave();

        return new ShakeDisplacement(
                   position: (wave * MaxPosDisplacement * Attenuation),
                   angle: (wave * MaxAngleDisplacement * Attenuation));
    }

    public float GetWave() => GetRawWave() * Amplitude.Calculate(Completeness, curve.attack, curve.release);
    public float GetRawWave() => Mathf.Sin(Completeness * WAVE_UNIT * curve.frequency);
}

public class ShakeData
{
    public Vector3 Direction;
    public Vector3 Angle;
    public Vector3 Axis;
}