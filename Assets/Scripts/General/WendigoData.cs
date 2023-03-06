using UnityEngine;

public class WendigoData 
{
    private IWendigoSerializableData serializable;
    private Transform transform;

    public WendigoData(IWendigoSerializableData serializable, Transform transform)
    {
        this.serializable = serializable;
        this.transform = transform;
    }

    public int Health { get { return serializable.Health; } set { serializable.Health = value; } }
    public bool IsAlive => Health > 0f;

    public float MovementSpeed => serializable.MovementSpeed;
    public float Deceleration => serializable.Deceleration;
    public float RotationSpeed => serializable.RotationSpeed;
    public Vector3 Velocity { get; set; }

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Quaternion Rotation { get { return transform.rotation; } set { transform.rotation = value; } }

    public Vector3 Right => transform.right;
    public Vector3 Up => transform.up;
    public Vector3 Forward => transform.forward;

    public bool IsActive { get; set; }
    public bool IsArrived { get; set; }


    public Transform Target { get { return serializable.Target; } set { serializable.Target = value; } }
    public bool TargetFitsLookAngle { get; set; }
    public bool TargetFitsFireballAngle { get; set; }
    

    public float FireballMinDistance => serializable.FireballMinDistance;
    public float FireballMaxDistance => serializable.FireballMaxDistance;

    public bool IsReadyToCast { get; set; }
    public bool IsReadyToBreathFire { get; set; }

    public float FireballCastTime => serializable.FireballCastTime;
    public float FireballChargeTime => serializable.FireballChargeTime;

    public bool FireballAbilityIsCharged { get; set; } = true;
    public bool FireballCastIsOver { get; set; } = false;

    

    public float LookAngleOfView => serializable.LookAngleOfView;
    public float AttackAngleOfView => serializable.AttackAngleOfView;


    public float FirebreathMinDistance => serializable.FirebreathMinDistance;
    public float FirebreathMaxDistance => serializable.FirebreathMaxDistance;
    public float FirebreathAngleOfView => serializable.FirebreathAngleOfView;
    public bool TargetFitsFirebreathAngle { get; set; }
    public bool FirebreathAbilityIsCharged { get; set; } = true;
    public bool FirebreathIsOver { get; set; } = false;
    public float FireRange => serializable.FireRange;
    public float FireScatter => serializable.FireScatter;
    public int FirePrecision => serializable.FirePrecision;
}

