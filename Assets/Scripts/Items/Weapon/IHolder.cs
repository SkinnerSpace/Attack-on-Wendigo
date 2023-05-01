using UnityEngine;

public interface IHolder
{
    Transform transform { get; }
    Vector3 targetPosition { get; }
}
