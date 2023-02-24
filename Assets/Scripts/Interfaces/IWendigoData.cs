using UnityEngine;

public interface IWendigoData
{
    float Health { get; set; }
    float MovementSpeed { get; }
    float Deceleration { get; }
    float RotationSpeed { get; }
    Vector3 Velocity { get; set; }

    Vector3 Position { get; }
    Quaternion Rotation { get; set; }

    Vector3 Right { get; } 
    Vector3 Up { get; }
    Vector3 Forward { get; }

    bool IsActive { get; set; }
    bool IsArrived { get; set; }
    Transform Target { get; set; }
}