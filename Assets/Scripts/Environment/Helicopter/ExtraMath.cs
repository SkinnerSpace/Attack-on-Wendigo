using UnityEngine;

public static class ExtraMath
{
    public static bool Between(this float value, float first, float second) => (value >= first) && (value <= second);
    public static float Remap(this float value, float xA, float xB, float yA, float yB)
    {
        float lerp = Mathf.InverseLerp(xA, xB, value);
        return Mathf.Lerp(yA, yB, lerp);
    }
}
