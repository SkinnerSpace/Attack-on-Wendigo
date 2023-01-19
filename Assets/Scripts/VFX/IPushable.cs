using UnityEngine;

public interface IPushable
{
    Vector3 position { get; }
    Vector3 direction { get; }
    void SetResistance(float resistance);
    void ApplyForce(Vector3 force);
}