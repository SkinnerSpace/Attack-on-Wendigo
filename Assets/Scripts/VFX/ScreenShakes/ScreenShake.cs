using UnityEngine;

public class ScreenShake
{
    private ShakeTimer timer;
    private Vector3 axis;
    private ShakeStrength strength;
    private ShakeCurve curve;
    private float attenuation = 1f;

    private ScreenShake()
    {
        timer = new ShakeTimer(1f);
        strength = new ShakeStrength(1f, 8f);
        curve = new ShakeCurve(10f, 0.25f, 0.25f);
        attenuation = 1f;
    }

    public static ScreenShake Create()
    {
        return new ScreenShake();
    }

    public ScreenShake withTime(float time)
    {
        timer.SetTime(time);
        return this;
    }

    public ScreenShake WithAxis(float x, float y, float z)
    {
        axis = new Vector3(x, y, z);
        return this;
    }

    public ScreenShake WithStrength(float amount, float angleMultiplier)
    {
        strength = new ShakeStrength(amount, angleMultiplier);
        return this;
    }

    public ScreenShake WithCurve(float frequency, float attack, float release)
    {
        curve = new ShakeCurve(frequency, attack, release);
        return this;
    }

    public ScreenShake WithAttenuation(float distance, float maxDistance)
    {
        attenuation = 1f - (distance / maxDistance);
        if (attenuation <= 0f) attenuation = 0f;
        else attenuation *= attenuation;

        return this;
    }

    public void Launch()
    {
        Shake shake = new Shake(timer, axis, strength, curve, attenuation);
        ShakeManager.Instance.AddAndLaunch(shake);
    }
}

