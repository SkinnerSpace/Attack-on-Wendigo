using UnityEngine;

public interface ITransform
{
    Vector3 Position { get; set; }
    Vector3 LocalPosition { get; set; }
    Vector3 LocalScale { get; set; }

    Vector3 Euler { get; set; }
    Vector3 LocalEuler { get; set; }
    Quaternion Rotation { get; set; }

    Vector3 Right { get; }
    Vector3 Up { get; }
    Vector3 Forward { get; }
}