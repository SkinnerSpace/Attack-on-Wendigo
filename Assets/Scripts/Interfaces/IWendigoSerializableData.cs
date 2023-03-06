using UnityEngine;

public interface IWendigoSerializableData
{
    int Health { get; set; }
    float MovementSpeed { get; set; }
    float RotationSpeed { get; set; }
    float Deceleration { get; set; }
    Transform Target { get; set; }
    float FireballMinDistance { get; }
    float FireballMaxDistance { get; }
    float FireballChargeTime { get; }
    float FireballCastTime { get; }

    float LookAngleOfView { get; }
    float AttackAngleOfView { get; }
    float FirebreathAngleOfView { get; }
    float FirebreathMinDistance { get; }
    float FirebreathMaxDistance { get; }

    float FireRange { get; }
    float FireScatter { get; }
    int FirePrecision { get; }
}

