using UnityEngine;

public static class Amplitude
{
    public static float Calculate(float percent, float attack, float release)
    {
        float amplitude = 1f;

        if (percent < attack){
            amplitude = GetSmoothAttack(percent, attack);
        }
        else if (percent > release){
            amplitude = GetSmoothRelease(percent, release);
        }

        return amplitude;
    }

    private static float GetSmoothAttack(float percent, float attack) => Mathf.Sqrt(percent / attack);
    private static float GetSmoothRelease(float percent, float release)
    {
        float amplitude = (1f - percent) / (1f - release);
        amplitude *= amplitude;

        return amplitude;
    }
}

