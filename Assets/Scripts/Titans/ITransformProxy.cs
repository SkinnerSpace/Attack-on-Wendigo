using UnityEngine;

public interface ITransformProxy
{
    Vector3 Position { get; set; }
    Vector3 LocalPosition { get; set; }
    Vector3 Angle { get; set; }
    Vector3 LocalAngle { get; set; }
    Quaternion Rotation { get; set; }
    Vector3 Forward { get; }
    Vector3 Right { get; }
}