﻿using UnityEngine;

public static class ExtraMath
{
    public static bool Between(this float value, float first, float second) => (value >= first) && (value <= second);
    public static float Remap(this float value, float Ax, float Ay, float Bx, float By)
    {
        float lerp = Mathf.InverseLerp(Ax, Ay, value);
        return Mathf.Lerp(Bx, By, lerp);
    }

    public static float Negative(this float value) => value * -1f;

    public static Vector2 FlatV2(this Vector3 vector) => new Vector2(vector.x, vector.z);

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

    public static float QuadEaseOut(float t) => 1f - QuadEaseIn(1f - t);

    public static float QuadEaseIn(float t) => t * t;

    public static float QuadInOut(float time) => time * time * (3 - (2 * time));

    public static Vector3 Average(this Vector3 original, Vector3 additional) => (original + additional) / 2f;

    public static float Clamp01(this float value) => Mathf.Clamp01(value);
}
