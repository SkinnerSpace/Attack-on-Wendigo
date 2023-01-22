using UnityEngine;

public static class ShakeHandler
{
    private const float WAVE_UNIT = Mathf.PI * 2f;
    private const float DEVIATION_MULTIPLIER = 0.1f;

    public static void Launch(Shake shake) => shake.Launch(GetRandDir(shake.Axis), GetRandAngle());

    public static void Handle(Shake shake)
    {
        MoveTheWave(shake);
        shake.Proceed();
    }

    public static void Handle(Shake shake, float progress)
    {
        MoveTheWave(shake);
        shake.SetCompleteness(progress);
    }

    public static void MoveTheWave(Shake shake)
    {
        shake.UpdateWave(GetRawWave(shake.Completeness, shake.FQ));

        if (shake.WaveHasPassed())
            shake.SetDir(ModifyDir(shake.Dir, shake.Axis));
    }

    public static ShakeDisplacement GetDisplacement(Shake shake)
    {
        float wave = GetWave(shake);

        return new ShakeDisplacement(
                   position: (wave * shake.MaxPosDisplacement * shake.Attenuation), 
                   angle: (wave * shake.MaxAngleDisplacement * shake.Attenuation));
    }

    private static float GetWave(Shake shake) => GetRawWave(shake.Completeness, shake.FQ) * 
                                                Amplitude.Calculate(shake.Completeness, shake.Attack, shake.Release);
    private static float GetRawWave(float time, float frequency) => Mathf.Sin(time * WAVE_UNIT * frequency);

    private static Vector3 ModifyDir(Vector3 dir, Vector3 axis)
    {
        Vector3 deviation = GetRandDir(axis) * DEVIATION_MULTIPLIER;
        Vector3 modifiedDir = (dir + deviation).normalized;

        return modifiedDir;
    }

    private static Vector3 GetRandDir(Vector3 axis)
    {
        return new Vector3(
               x: Rand.Range(-1f, 1f) * axis.x,
               y: Rand.Range(-1f, 1f) * axis.y,
               z: Rand.Range(-1f, 1f) * axis.z).
               normalized;
    }

    private static float GetRandAngle() => (Rand.Range(0f, 1f) < 0.5f) ? -1 : 1f;
}

