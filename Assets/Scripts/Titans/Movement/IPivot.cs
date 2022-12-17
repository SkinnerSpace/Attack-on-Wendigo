using UnityEngine;

public interface IPivot
{
    Vector3 Position { get; }
    Vector3 Forward { get; }
    Vector3 Right { get; }
}
