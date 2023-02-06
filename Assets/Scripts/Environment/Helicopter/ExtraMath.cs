using UnityEngine;

public static class ExtraMath
{
    public static bool Between(this float value, float first, float second) => (value >= first) && (value <= second);
    public static float Remap(this float value, float Ax, float Ay, float Bx, float By)
    {
        float lerp = Mathf.InverseLerp(Ax, Ay, value);
        return Mathf.Lerp(Bx, By, lerp);
    }
}
