using UnityEngine;

public class MockCharacterData : ICharacterData
{
    public Vector3 Position { get; set; }
    public float Height { get; set; }

    public Vector3 Velocity => new Vector3(FlatVelocity.x, VerticalVelocity, FlatVelocity.y);
    public Vector2 FlatVelocity { get; set; }
    public float VerticalVelocity { get; set; }

    public float Gravity { get; set; }

    public float JumpHeight { get; set; }
    public int JumpCount { get; set; }
    public int MaxJumpCount { get; set; }
    public bool IsJumping { get; set; }

    public bool WasGrounded { get; set; }
    public bool IsGrounded { get; set; }
    public float GroundDetectionRadius { get; set; }
    public float GroundDetectionHeight { get; set; }

    public Vector3 Forward { get; set; } = Vector3.forward;
    public Vector3 Right { get; set; } = Vector3.right;
    public Vector3 Up { get; set; } = Vector3.up;
    public Vector3 Euler { get; set; }

    public Camera Cam { get; set; }
    public Quaternion CameraRotation { get; set; }
    public Quaternion CameraViewRotation { get; set; }
    public Vector3 CameraEuler { get; set; }
    public Vector3 CameraViewEuler { get; set; }
    public Vector3 CameraLocalPos { get; set; }
    public Vector3 CameraTiltEuler { get; set; }
    public float TiltSpeed { get; set; } 
    public float TiltMaxAngle { get; set; }

    public float Speed { get; set; }

    public bool IsAbleToDash { get; set; }
    public Vector2 DashDirection { get; set; }
    public float DashDistance { get; set; }
    public float DashCoolDownTime { get; set; }

    public float Deceleration { get; set; }
    public float GroundDeceleration { get; set; }
    public float AirDeceleration { get; set; }

    public float DampedSpringPower { get; set; }
    public float DampedSpringTime { get; set; }
    public float DampedSpringAmplitude { get; set; }
    public float CurrentDampedSpringTime { get; set; }

    public float MinFOV { get; set; }
    public float MaxFOV { get; set; }
    public float FOV { get; set; }
    public float FOVChangeSpeed { get; set; }
    public float FOVPower { get; set; }
}
