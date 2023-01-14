using UnityEngine;

public static class ShakeHandler
{
    private const float WAVE_UNIT = Mathf.PI * 2f;
    private const float DEVIATION_MULTIPLIER = 0.1f;

    public static void Launch(Shake shake) => shake.Launch(GetRandDir(), GetRandAngle());

    public static void Handle(Shake shake)
    {
        shake.UpdateWave(GetRawWave(shake.Time, shake.FQ));

        if (shake.WaveHasPassed())
            shake.SetDir(ModifyDir(shake.Dir));

        shake.Proceed();
    }

    public static ShakeDisplacement GetDisplacement(Shake shake)
    {
        float wave = GetWave(shake);

        return new ShakeDisplacement(
                   position: (wave * shake.MaxPosDisplacement * shake.Attenuation), 
                   angle: (wave * shake.MaxAngleDisplacement * shake.Attenuation));
    }

    private static float GetWave(Shake shake) => GetRawWave(shake.Time, shake.FQ) * 
                                                Amplitude.Calculate(shake.Time, shake.Attack, shake.Release);
    private static float GetRawWave(float time, float frequency) => Mathf.Sin(time * WAVE_UNIT * frequency);

    private static Vector3 ModifyDir(Vector3 dir)
    {
        Vector3 deviation = GetRandDir() * DEVIATION_MULTIPLIER;
        Vector3 modifiedDir = (dir + deviation).normalized;

        return modifiedDir;
    }

    private static Vector3 GetRandDir()
    {
        return new Vector3(
               x: Rand.Range(-1f, 1f),
               y: Rand.Range(-1f, 1f),
               z: 0f).
               normalized;
    }

    private static float GetRandAngle() => (Rand.Range(0f, 1f) < 0.5f) ? -1 : 1f;
}

