using UnityEngine;

[CreateAssetMenu(fileName ="WendigoSpawnConfig", menuName = "ScriptableObjects/WendigoSpawnConfig")]
public class WendigoSpawnConfig : ScriptableObject
{
    public int initialCount;

    public AnimationCurve concurrentSpawnCount;
    public AnimationCurve minTimeInterval;
    public AnimationCurve maxTimeInterval;
    public AnimationCurve health;
    public AnimationCurve speed;
}

