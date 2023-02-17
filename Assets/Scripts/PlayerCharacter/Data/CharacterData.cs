using System;
using UnityEngine;

[Serializable]
public class CharacterData : MonoBehaviour, ICharacterData
{
    [Header("Movement")]
    [SerializeField] private float speed = 300f;
    [SerializeField] private float groundDeceleration = 20f;
    [SerializeField] private float airDeceleration = 15f;

    [Header("Dash")]
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashCoolDownTime = 0.3f;

    [Header("Jump")]
    [SerializeField] private float gravity = 26f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private int maxJumpCount = 2;

    [Header("Ground Detection")]
    [SerializeField] private float groundDetectionRadius = 0.6f;
    [SerializeField] private float groundDetectionHeight = 0.5f;

    [Header("Damped Spring")]
    [SerializeField] private float dampedSpringPower = 1f;
    [SerializeField] private float dampedSpringTime = 0.5f;

    [Header("FOV")]
    [SerializeField] private float minFOV = 80f;
    [SerializeField] private float maxFOV = 120f;
    [SerializeField] private float fOVChangeSpeed = 10f;

    [Header("Camera tilt")]
    [SerializeField] private float tiltSpeed = 5f;
    [SerializeField] private float tiltMaxAngle = 5f;

    [Header("Components")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera cam;

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 Forward => transform.forward;
    public Vector3 Right => transform.right;
    public Vector3 Up => transform.up;
    public Vector3 Euler { get { return transform.eulerAngles; } set { transform.eulerAngles = value; } }

    public Camera Cam => cam;
    public CharacterController Controller => controller;

    public Vector3 CameraViewEuler { get; set; }
    public Quaternion CameraRotation { get { return cam.transform.rotation; } set { cam.transform.rotation = value; } }
    public Quaternion CameraViewRotation { get; set; }
    public Vector3 CameraEuler { get { return cam.transform.eulerAngles; } set { cam.transform.eulerAngles = value; } }
    public Vector3 CameraLocalPos { get { return cam.transform.localPosition; } set { cam.transform.localPosition = value; } }
    public Vector3 CameraTiltEuler { get; set; }
    public float TiltSpeed => tiltSpeed;
    public float TiltMaxAngle => tiltMaxAngle;

    public float Height => controller.height;


    public float Speed => speed;
    public float Deceleration { get; set; }
    public float GroundDeceleration => groundDeceleration;
    public float AirDeceleration => airDeceleration;

    public bool IsAbleToDash { get; set; }
    public Vector2 DashDirection { get; set; }
    public float DashDistance => dashDistance;
    public float DashCoolDownTime => dashCoolDownTime;
    public Vector3 Velocity { get; set; }

    public Vector2 FlatVelocity { get { return flatVelocity; } set { flatVelocity = value; } }
    private Vector2 flatVelocity;

    public float PreviousVerticalVelocity { get; set; }
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

    public float DampedSpringPower => dampedSpringPower;
    public float DampedSpringTime => dampedSpringTime;
    public float DampedSpringAmplitude { get; set; }
    public float CurrentDampedSpringTime { get; set; }

    public float MinFOV => minFOV;
    public float MaxFOV => maxFOV;
    public float FOV { get { return cam.fieldOfView; } set { cam.fieldOfView = value; } }
    public float FOVChangeSpeed => fOVChangeSpeed;
    public float FOVPower { get; set; }
}

