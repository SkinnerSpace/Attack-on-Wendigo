public static class Rand
{
    public static float Range(float min, float max) => UnityEngine.Random.Range(min, max);
    public static int Range(int min, int max) => UnityEngine.Random.Range(min, max);
    public static float Range01() => UnityEngine.Random.Range(0f, 1f);
    public static float GetBisigned() => UnityEngine.Random.Range(-1f, 1f);
}

