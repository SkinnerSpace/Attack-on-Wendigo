using UnityEngine;

public class ShakeBuilder
{
    private ShakeTimer timer;
    private ShakeStrength strength;
    private ShakeCurve curve;
    private float attenuation = 1f;

    private ShakeBuilder()
    {
        timer = new ShakeTimer(1f);
        strength = new ShakeStrength(1f, 8f);
        curve = new ShakeCurve(10f, 0.25f, 0.25f);
        attenuation = 1f;
    }

    public static ShakeBuilder Create()
    {
        return new ShakeBuilder();
    }

    public ShakeBuilder withTime(float time)
    {
        timer.SetTime(time);
        return this;
    }

    public ShakeBuilder WithStrength(float amount, float angleMultiplier)
    {
        strength = new ShakeStrength(amount, angleMultiplier);
        return this;
    }

    public ShakeBuilder WithCurve(float frequency, float attack, float release)
    {
        curve = new ShakeCurve(frequency, attack, release);
        return this;
    }

    public ShakeBuilder WithAttenuation(float distance, float maxDistance)
    {
        attenuation = 1f - (distance / maxDistance);
        attenuation = Mathf.Min(attenuation, 1f);
        attenuation *= attenuation;

        return this;
    }

    public void Launch()
    {
        ScreenShake shake = new ScreenShake(timer, strength, curve, attenuation);
        ScreenShakeController.Instance.AddAndLaunch(shake);
    }
}

