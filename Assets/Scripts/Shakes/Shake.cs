using UnityEngine;
using System;

public class Shake : IShake
{
    public bool IsActive { get; private set; }

    public ShakeData data;
    public ShakeAttenuation Attenuation { get; private set; }
    public ShakeWave Wave { get; private set; }
    private ShakeStrength strength;
    private ShakeTimer timer;

    public Shake(Vector3 axis, ShakeStrength strength, ShakeCurve curve, ShakeAttenuation attenuation, ShakeTimer timer = null)
    {
        data.Axis = axis;
        this.strength = strength;
        Wave = new ShakeWave(curve);
        Attenuation = attenuation;
        this.timer = timer;

        Activate();
    }

    private void Activate()
    {
        data.Direction = ShakeUtils.DivertVector(data.Axis);
        data.Angle = ShakeUtils.GetRandAngle();
        IsActive = true;
    }

    public void Update()
    {
        timer.CountDown();
        Wave.Update(timer.Progress);
        IsActive = timer.Progress < 1f;

        UpdateDirectionIfTheWaveHasPassed();
    }

    public void Update(float progress)
    {
        Wave.Update(progress);
        IsActive = progress < 1f;

        UpdateDirectionIfTheWaveHasPassed();
    }

    private void UpdateDirectionIfTheWaveHasPassed()
    {
        if (Wave.HasPassed()){
            data.Direction = ShakeUtils.DivertDirection(data.Direction, data.Axis);
        }
    }

    public IShakeDisplacement GetDisplacement()
    {
        return new ShakeDisplacement(
                   position: (Wave.Value * GetMaxPosDisplacement() * Attenuation.Amount),
                   angle: (Wave.Value * GetMaxAngleDisplacement() * Attenuation.Amount));
    }

    public Vector3 GetMaxPosDisplacement() => data.Direction * strength.amount;
    public Vector3 GetMaxAngleDisplacement() => data.Angle * strength.amount * strength.angleMultiplier;
}
