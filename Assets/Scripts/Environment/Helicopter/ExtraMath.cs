using UnityEngine;

public static class ExtraMath
{
    public static bool Between(this float value, float first, float second) => (value >= first) && (value <= second);
    public static float Remap(this float value, float Ax, float Ay, float Bx, float By)
    {
        float lerp = Mathf.InverseLerp(Ax, Ay, value);
        return Mathf.Lerp(Bx, By, lerp);
    }

    public static float QuadEaseOut(float t) => 1f - QuadEaseIn(1f - t);

    public static float QuadEaseIn(float t) => t * t;

    public static float QuadInOut(float time) => time * time * (3 - (2 * time));

    public static Vector3 Mean(this Vector3 original, Vector3 additional) => (original + additional) / 2f;

    public static float Clamp01(this float value) => Mathf.Clamp01(value);
}
