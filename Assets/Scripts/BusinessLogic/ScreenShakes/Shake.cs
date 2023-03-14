using UnityEngine;

public class Shake
{
    public bool isActive { get; private set; }
    public float Completeness { get; private set; }

    public Vector3 MaxPosDisplacement => direction * strength.amount;
    public Vector3 MaxAngleDisplacement => angle * strength.amount * strength.angleMultiplier;
    public Vector3 Dir => direction;
    public Vector3 Axis => axis;
    public float Attenuation
    {
        get{
            if (recipient != null)
                CalculateAttenuation();

            return attenuation;
        }
    }

    public float FQ => curve.frequency;
    public float Attack => curve.attack;
    public float Release => curve.release;

    private Vector3 direction;
    private Vector3 axis;
    private Vector3 angle;
    private float attenuation;

    private ShakeStrength strength;
    private ShakeCurve curve;

    private ShakeTimer timer;

    private float exWave;
    private float wave;

    private Transform recipient;
    private float maxDistance;
    private Vector3 position;

    public Shake(Vector3 axis, ShakeStrength strength, ShakeCurve curve, float attenuation, ShakeTimer timer = null)
    {
        this.axis = axis;
        this.strength = strength;
        this.curve = curve;
        this.attenuation = attenuation;
        this.timer = timer;
    }

    public Shake(Vector3 axis, ShakeStrength strength, ShakeCurve curve, Transform recipient, Vector3 position, float maxDistance, ShakeTimer timer = null)
    {
        this.axis = axis;
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

    public void Launch(Vector3 dir, float angle)
    {
        if (!isActive)
        {
            SetDir(dir);
            SetAngle(angle);
            isActive = true;
        }
    }

    public void Proceed()
    {
        timer.CountDown();

        Completeness = timer.TimeScaled;
        isActive = !timer.TimeOut();
    }

    public void SetCompleteness(float completeness)
    {
        Completeness = completeness;
        isActive = completeness < 1f;
    }

    public void UpdateWave(float wave)
    {
        exWave = this.wave;
        this.wave = wave;
    }

    public bool WaveHasPassed() => (exWave < 0f) && (wave >= 0f);
    public void SetAxis(Vector3 axis) => this.axis = axis;
    public void SetDir(Vector3 direction) => this.direction = direction;
    public void SetAngle(float zAngle) => angle = new Vector3(0f, 0f, zAngle);
}
