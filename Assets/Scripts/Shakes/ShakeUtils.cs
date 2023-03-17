using UnityEngine;

public static class ShakeUtils
{
    private static float DEVIATION_MULTIPLIER = 0.1f;

    public static Vector3 DivertDirection(Vector3 direction, Vector3 axis)
    {
        Vector3 deviation = DivertVector(axis) * DEVIATION_MULTIPLIER;
        Vector3 modifiedDir = (direction + deviation).normalized;

        return modifiedDir;
    }

    public static Vector3 DivertVector(Vector3 vector)
    {
        return new Vector3(
               x: Rand.Range(-1f, 1f) * vector.x,
               y: Rand.Range(-1f, 1f) * vector.y,
               z: Rand.Range(-1f, 1f) * vector.z).
               normalized;
    }

    public static Vector3 GetRandAngle()
    {
        float zAngle = (Rand.Range(0f, 1f) < 0.5f) ? -1 : 1f;
        Vector3 randomAngle = new Vector3(0f, 0f, zAngle);
        return randomAngle;
    }
}