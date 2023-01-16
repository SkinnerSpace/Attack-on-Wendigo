using UnityEngine;

public class PropShaker
{
    private float power = 0.5f; 
    private float attack = 0.1f;
    private float release = 0.25f;

    private Shake shake;

    public void PrepareToShake(CollapseEstimator estimator)
    {
        ShakeTimer timer = new ShakeTimer(estimator.time);
        Vector3 axis = new Vector3(1f, 0f, 1f);
        ShakeStrength strength = new ShakeStrength(power, 0f);
        ShakeCurve curve = new ShakeCurve(estimator.frequency, attack, release);
        float attenuation = 1f;

        shake = new Shake(timer, axis, strength, curve, attenuation);
    }

    public void Launch() => ShakeHandler.Launch(shake);

    public void UpdateShake() => ShakeHandler.Handle(shake);
    public Vector3 GetPosDisplacement() => ShakeHandler.GetDisplacement(shake).position;
    public bool IsDone() => !shake.isActive;
}

