using UnityEngine;

public interface IPushable
{
    Vector3 position { get; }
    void Push(Vector3 pushVelocity);
}