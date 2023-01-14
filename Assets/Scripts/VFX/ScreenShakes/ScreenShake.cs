using UnityEngine;

public class ScreenShake
{
    public bool isActive { get; private set; }

    public float Time => timer.TimeScaled;

    public Vector3 MaxPosDisplacement => direction * strength.amount;
    public Vector3 MaxAngleDisplacement => angle * strength.amount * strength.angleMultiplier;
    public Vector3 Dir => direction;

    public float FQ => curve.frequency;
    public float Attack => curve.attack;
    public float Release => curve.release;

    private Vector3 direction;
    private Vector3 angle;

    private ShakeStrength strength;
    private ShakeCurve curve;

    private ShakeTimer timer;

    private float exWave;
    private float wave;

    public ScreenShake(float time, ShakeStrength strength, ShakeCurve curve)
    {
        this.strength = strength;
        this.curve = curve;

        timer = new ShakeTimer(time);
    }

    public void Launch(Vector3 dir, float angle)
    {
        SetDir(dir);
        SetAngle(angle);
        isActive = true;
    }

    public void Proceed()
    {
        timer.CountDown();
        isActive = !timer.TimeOut();
    }

    public void UpdateWave(float wave)
    {
        exWave = this.wave;
        this.wave = wave;
    }

    public bool WaveHasPassed() => (exWave < 0f) && (wave >= 0f);
    public void SetDir(Vector3 direction) => this.direction = direction;
    public void SetAngle(float zAngle) => new Vector3(0f, 0f, zAngle);
}

