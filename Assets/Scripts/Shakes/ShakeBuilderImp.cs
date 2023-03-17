using UnityEngine;

public class ShakeBuilderImp : IShakeBuilder
{
    private ShakeTimer timer;
    private Vector3 axis;
    private ShakeStrength strength;
    private ShakeCurve curve;
    private ShakeAttenuation attenuation;

    public ShakeBuilderImp()
    {
        timer = new ShakeTimer(1f);
        strength = new ShakeStrength(1f, 8f);
        curve = new ShakeCurve(10f, 0.25f, 0.25f);
        attenuation = new ShakeAttenuation(1f);
    }

    public static IShakeBuilder Create() => new ShakeBuilderImp();

    public IShakeBuilder withTime(float time)
    {
        timer.SetWaitTime(time);
        return this;
    }

    public IShakeBuilder WithAxis(float x, float y, float z)
    {
        axis = new Vector3(x, y, z);
        return this;
    }

    public IShakeBuilder WithStrength(float amount, float angleMultiplier)
    {
        strength = new ShakeStrength(amount, angleMultiplier);
        return this;
    }

    public IShakeBuilder WithCurve(float frequency, float attack, float release)
    {
        ShakeCurve curve = new ShakeCurve(frequency, attack, release);
        return this;
    }

    public IShakeBuilder WithAttenuation(Vector3 position, Transform subject, float maxDistance)
    {
        attenuation.SetUp(position, subject, maxDistance);
        return this;
    }

    public void BuildAndLaunch(IShakeManager shakeManager)
    {
        IShake shake = Build();
        shakeManager.AddAndLaunch(shake);
    }

    public IShake Build() => new Shake(axis, strength, curve, attenuation, timer);
}
