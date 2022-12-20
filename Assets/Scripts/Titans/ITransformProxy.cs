using UnityEngine;

public interface ITransformProxy
{
    Vector3 Position { get; set; }
    Vector3 Forward { get; }
    Vector3 Right { get; }
}