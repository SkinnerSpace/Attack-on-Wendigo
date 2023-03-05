using UnityEngine;

public static class VectorExtention
{
    public static Vector2 Flatten(this Vector3 v) => new Vector2(v.x, v.z);
}