﻿using System;
using UnityEngine;

[Serializable]
public class CharacterData : MonoBehaviour, ICharacterData
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float groundDeceleration;
    [SerializeField] private float airDeceleration;
    [SerializeField] private float dashDistance;

    [Header("Jump")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private int maxJumpCount;

    [Header("Ground Detection")]
    [SerializeField] private float groundDetectionRadius;
    [SerializeField] private float groundDetectionHeight;

    [Header("Components")]
    [SerializeField] private CharacterController controller;

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 Forward => transform.forward;

    public Vector3 Right => transform.right;

    public Vector3 Up => transform.up;


    public float Height => controller.height;


    public float Speed => speed;
    public float Deceleration { get; set; }
    public float GroundDeceleration => groundDeceleration;
    public float AirDeceleration => airDeceleration;
    public float DashDistance => dashDistance;
    public Vector3 Velocity => new Vector3(flatVelocity.x, verticalVelocity, flatVelocity.y);

    public Vector2 FlatVelocity { get { return flatVelocity; } set { flatVelocity = value; } }
    private Vector2 flatVelocity;

    public float VerticalVelocity { get { return verticalVelocity; } set { verticalVelocity = value; } }
    private float verticalVelocity;

    public float Gravity => gravity;
    public float JumpHeight => jumpHeight;
    public int JumpCount { get; set; }
    public int MaxJumpCount => maxJumpCount;
    public bool IsJumping { get; set; }

    public bool WasGrounded { get; set; }
    public bool IsGrounded { get; set; }

    public float GroundDetectionRadius { get { return groundDetectionRadius; } set { groundDetectionRadius = value; } }
    public float GroundDetectionHeight { get { return groundDetectionHeight; } set { groundDetectionHeight = value; } }
}
