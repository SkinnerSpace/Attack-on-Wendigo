using System;
using UnityEngine;

[Serializable]
public class CharacterData : MonoBehaviour, ICharacterData
{
    public static CharacterData Instance;

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

    [Header("Interaction")]
    [SerializeField] private float reachDistance = 3f;
    [SerializeField] private float dropItemStrength = 1000f;

    [Header("Ground Detection")]
    [SerializeField] private float groundDetectionRadius = 0.6f;
    [SerializeField] private float groundDetectionHeight = 0.5f;


    [SerializeField] private DampedSpringData dampedSpringData;
    public IDampedSpringData DampedSpring => dampedSpringData;

    [Header("FOV")]
    [SerializeField] private float defaultFOV = 80f;
    [SerializeField] private float additionalFOV = 40f;

    [Header("Camera tilt")]
    [SerializeField] private float tiltSpeed = 5f;
    [SerializeField] private float tiltMaxAngle = 5f;

    [Header("Components")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera cam;

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 CameraDampedPos { get; set; }

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

    public Vector3 ShakePosition { get; set; }
    public Vector3 ShakeEuler { get; set; }


    public Vector3 CameraForward => transform.forward;
    public Vector3 CameraRight => transform.right;
    public Vector3 CameraUp => transform.up;

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


    public float FOV { get { return cam.fieldOfView; } set { cam.fieldOfView = value; } }
    public float DefaultFOV => defaultFOV;
    public float AdditionalFOV => additionalFOV;


    public float ReachDistance => reachDistance;
    public float DropItemStrength => dropItemStrength;

    private void Awake()
    {
        Instance = this;
    }
}

