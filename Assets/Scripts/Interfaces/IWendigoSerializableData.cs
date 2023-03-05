using UnityEngine;

public interface IWendigoSerializableData
{
    int Health { get; set; }
    float MovementSpeed { get; set; }
    float RotationSpeed { get; set; }
    float Deceleration { get; set; }
    Transform Target { get; set; }
    GameObject Fireball { get; }
    float FireballMinDistance { get; }
    float FireballMaxDistance { get; }
    float FireballChargeTime { get; }
    float FireballCastTime { get; }

    float LookAngleOfView { get; }
    float AttackAngleOfView { get; }
}

