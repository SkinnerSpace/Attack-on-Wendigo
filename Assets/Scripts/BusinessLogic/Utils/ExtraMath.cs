using UnityEngine;

public static class ExtraMath
{
    public const float MAX_SIN_TIME = Mathf.PI * 2f;

    public static bool Between(this float value, float first, float second) => (value >= first) && (value <= second);
    public static float Remap(this float value, float Ax, float Ay, float Bx, float By)
    {
        float lerp = Mathf.InverseLerp(Ax, Ay, value);
        return Mathf.Lerp(Bx, By, lerp);
    }

    public static float Negative(this float value) => value * -1f;

    public static Vector2 FlatV2(this Vector3 vector) => new Vector2(vector.x, vector.z);
    public static Vector3 FlatV3(this Vector3 vector) => new Vector3(vector.x, 0f, vector.z);
    public static Vector3 OnlyY(this Vector3 vector) => new Vector3(0f, vector.y, 0f);
    public static Vector3 OnlyYZ(this Vector3 vector) => new Vector3(0f, vector.y, vector.z);
    public static Vector3 GetRandomVector3() => new Vector3(x: Rand.Range(-1f, 1f), y: Rand.Range(-1f, 1f), z: Rand.Range(-1f, 1f)).normalized;
    public static Vector3 Abs(this Vector3 v) => new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    public static Vector3 ToVector3(this float value) => new Vector3(value, value, value);
    public static float[] ToArray(this Vector3 v) => new float[] {v.x, v.y, v.z};

    public static float Round(this float value, float digits) => Mathf.Round(value * 10f * digits) / (10f * digits);

    public static Vector3 Round(this Vector3 vector, int size)
    {
        vector.x = Mathf.Round(vector.x / size) * size;
        vector.y = Mathf.Round(vector.y / size) * size;
        vector.z = Mathf.Round(vector.z / size) * size;
        return vector;
    }

    public static Vector2 Round(this Vector2 vector, int size)
    {
        vector.x = Mathf.Round(vector.x / size) * size;
        vector.y = Mathf.Round(vector.y / size) * size;
        return vector;
    }

    public static Vector3 Average(this Vector3 original, Vector3 additional) => (original + additional) / 2f;
    public static Vector3 SetY(this Vector3 original, float y) => new Vector3(original.x, y, original.z);
    public static float AverageWith(this short a, short b) => (a + b) / 2f;

    public static float Clamp01(this float value) => Mathf.Clamp01(value);
}
