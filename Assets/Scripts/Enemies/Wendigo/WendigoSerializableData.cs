using UnityEngine;

public class WendigoSerializableData : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float deceleration;
    [SerializeField] private Transform target;

    [Header("Fireball")]
    [SerializeField] private float fireballMinDistance;
    [SerializeField] private float fireballMaxDistance;
    [SerializeField] private float fireballChargeTime;
    [SerializeField] private float fireballCastTime;

    [Header("Firebreath")]
    [SerializeField] private float firebreathMinDistance;
    [SerializeField] private float firebreathMaxDistance;
    [SerializeField] private float fireRange;
    [Range(0f, 1f)]
    [SerializeField] private float fireScatter;
    [SerializeField] private int firePrecision;

    [Header("Angle of View")]
    [SerializeField] private float lookAngleOfView;
    [SerializeField] private float attackAngleOfView;
    [SerializeField] private float firebreathAngleOfView;

    public int Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public float Deceleration { get { return deceleration; } set { deceleration = value; } }
    public Transform Target { get { return target; } set { target = value; } }

    public float FireballMinDistance => fireballMinDistance;
    public float FireballMaxDistance => fireballMaxDistance;
    public float FireballChargeTime => fireballChargeTime;
    public float FireballCastTime => fireballCastTime;

    public float FirebreathMinDistance => firebreathMinDistance;
    public float FirebreathMaxDistance => firebreathMaxDistance;

    public float LookAngleOfView => lookAngleOfView;
    public float AttackAngleOfView => attackAngleOfView;
    public float FirebreathAngleOfView => firebreathAngleOfView;

    public float FireRange => fireRange;
    public float FireScatter => fireScatter;
    public int FirePrecision => firePrecision;
}
