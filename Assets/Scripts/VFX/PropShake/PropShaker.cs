using UnityEngine;

public class PropShaker : ICollapseObserver
{
    private float power = 0.5f; 
    private float attack = 0.1f;
    private float release = 0.25f;

    private Shake shake;

    public PropShaker(float frequency)
    {
        Vector3 axis = new Vector3(1f, 0f, 1f);
        ShakeStrength strength = new ShakeStrength(power, 0f);
        ShakeCurve curve = new ShakeCurve(frequency, attack, release);
        float attenuation = 1f;

        shake = new Shake(axis, strength, curve, attenuation);
    }

    public void Launch() => ShakeHandler.Launch(shake);
    public void ReceiveCollapseUpdate(float progress) => ShakeHandler.Handle(shake, progress);
    public Vector3 GetPosDisplacement() => ShakeHandler.GetDisplacement(shake).position;
}

