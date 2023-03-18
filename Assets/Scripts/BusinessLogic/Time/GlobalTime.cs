using UnityEngine;

public static class GlobalTime
{
    private static float _fixedDeltaTime = 0f;

    public static float DeltaTime => (_fixedDeltaTime == 0f) ? Time.deltaTime : _fixedDeltaTime;

    public static void SetDeltaTime(float fixedDeltaTime) => _fixedDeltaTime = fixedDeltaTime;
}