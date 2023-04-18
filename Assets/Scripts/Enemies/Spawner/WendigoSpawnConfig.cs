using UnityEngine;

[CreateAssetMenu(fileName ="WendigoSpawnConfig", menuName = "ScriptableObjects/WendigoSpawnConfig")]
public class WendigoSpawnConfig : ScriptableObject
{
    public int initialCount;

    [Header("Spawner")]
    public AnimationCurve concurrentSpawnCount;
    public AnimationCurve minTimeInterval;
    public AnimationCurve maxTimeInterval;

    [Header("Basic Characteristics")]
    public AnimationCurve health;
    public AnimationCurve speed;
    public AnimationCurve maxFireballDistance;
}

