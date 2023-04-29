using UnityEngine;

public static class Rand
{
    public static float Range(float min, float max) => Random.Range(min, max);
    public static int Range(int min, int max) => Random.Range(min, max);
    public static float Range01() => Random.Range(0f, 1f);
    public static float GetBisigned() => Random.Range(-1f, 1f);
    public static Vector3 GetCircularDirection(){
        Vector3 direction = Random.insideUnitCircle.normalized;
        direction = new Vector3(direction.x, 0f, direction.y);
        return direction;
    }
}

