public static class Rand
{
    public static float Range(float min, float max) => UnityEngine.Random.Range(min, max);
    public static int Range(int min, int max) => UnityEngine.Random.Range(min, max);
    public static float GetBisigned() => UnityEngine.Random.Range(-1f, 1f);
}

