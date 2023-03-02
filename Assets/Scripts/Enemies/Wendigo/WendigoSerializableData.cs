﻿using UnityEngine;

public class WendigoSerializableData : MonoBehaviour, IWendigoSerializableData
{
    [SerializeField] private int health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float deceleration;
    [SerializeField] private Transform target;

    [Header("Combat")]
    [SerializeField] private GameObject fireball;
    [SerializeField] private float fireballMinDistance;
    [SerializeField] private float fireballMaxDistance;
    [SerializeField] private float fireballChargeTime;
    [SerializeField] private float fireballCastTime;

    public int Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public float Deceleration { get { return deceleration; } set { deceleration = value; } }
    public Transform Target { get { return target; } set { target = value; } }
    public GameObject Fireball => fireball;
    public float FireballMinDistance => fireballMinDistance;
    public float FireballMaxDistance => fireballMaxDistance;
    public float FireballChargeTime => fireballChargeTime;
    public float FireballCastTime => fireballCastTime;
}
