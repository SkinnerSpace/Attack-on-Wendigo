using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private CapsuleCollider collisionBox;

    [Header("Horizontal movement")]
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float dashDistance = 10f;

    public float Speed { get; set; }
    public float MinSpeed => minSpeed;
    public float MaxSpeed => maxSpeed;
    public float Acceleration => acceleration;
    public float Deceleration => deceleration;
    
    [Header("Vertical movement")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = 16f;

    public float JumpHeight => jumpHeight;
    public float DashDistance => dashDistance;
    public float Gravity => gravity;
    
    public float height => collisionBox.height;

    private void Awake()
    {
        collisionBox = GetComponentInChildren<CapsuleCollider>();
        Speed = MinSpeed;
    }
}
