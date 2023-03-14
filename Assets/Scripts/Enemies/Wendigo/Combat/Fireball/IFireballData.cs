using UnityEngine;

public interface IFireballData
{
    Vector3 Position { get; set; }
    Vector3 Forward { get; }
    float Speed { get; }
    float CollisionRadius { get; }
    float ExplosionRadius { get; }
    int Damage { get; }
    float Impact { get; }
}