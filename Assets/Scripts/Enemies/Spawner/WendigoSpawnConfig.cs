using UnityEngine;

[CreateAssetMenu(fileName ="WendigoSpawnConfig", menuName = "ScriptableObjects/WendigoSpawnConfig")]
public class WendigoSpawnConfig : ScriptableObject
{
    public int initialCount = 9;

    [Header("Spawner")]
    public AnimationCurve concurrentSpawnCount;
    public AnimationCurve minTimeInterval;
    public AnimationCurve maxTimeInterval;

    [Header("Basic Characteristics")]
    public AnimationCurve health;
    public AnimationCurve speed;
    public AnimationCurve maxFireballDistance;

    [Header("Boss")]
    public float healthMultiplier = 1.5f;
    public float speedMultiplier = 1.25f;
    public float fireballDistanceMultiplier = 1.25f;
}

