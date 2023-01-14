using UnityEngine;

public static class Amplitude
{
    public static float Calculate(float time, float attack, float release)
    {
        float amplitude = 1f;

        if (time < attack)
        {
            amplitude = GetSmoothAttack(time, attack);
        }
        else if (time > release)
        {
            amplitude = GetSmoothRelease(time, release);
        }

        return amplitude;
    }

    private static float GetSmoothAttack(float time, float attack) => Mathf.Sqrt(time / attack);
    private static float GetSmoothRelease(float time, float release)
    {
        float amplitude = (1f - time) / (1f - release);
        amplitude *= amplitude;

        return amplitude;
    }
}

