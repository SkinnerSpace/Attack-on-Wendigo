using UnityEngine;

public class MockCharacterData : ICharacterData
{
    public Vector3 Position { get; set; }
    public float Height { get; set; }


    public int Health { get; set; }


    public Vector3 Velocity { get; private set; }
    public Vector2 FlatVelocity { get; set; }
    public float VerticalVelocity { get; set; }
    public float PreviousVerticalVelocity { get; set; }
    public void AddVelocity(Vector3 inVelocity)
    {
        FlatVelocity += inVelocity.FlatV2();
        VerticalVelocity += inVelocity.y;
    }
    public void UpdateVelocity()
    {
        PreviousVerticalVelocity = VerticalVelocity;
        Velocity = new Vector3(FlatVelocity.x, VerticalVelocity, FlatVelocity.y);
    }

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
    public Vector3 CameraDampedPos { get; set; }
    public Vector3 CameraTiltEuler { get; set; }
    public float TiltSpeed { get; set; } 
    public float TiltMaxAngle { get; set; }

    public Vector3 ShakePosition { get; set; }
    public Vector3 ShakeEuler { get; set; }

    public Vector3 CameraForward { get; set; } = Vector3.forward;
    public Vector3 CameraRight { get; set; } = Vector3.right;
    public Vector3 CameraUp { get; set; } = Vector3.up;


    public float Speed { get; set; }

    public bool IsAbleToDash { get; set; }
    public Vector2 DashDirection { get; set; }
    public float DashDistance { get; set; }
    public float DashCoolDownTime { get; set; }

    public float Deceleration { get; set; }
    public float GroundDeceleration { get; set; }
    public float AirDeceleration { get; set; }

    public IDampedSpringData DampedSpring { get; set; }


    public float FOV { get; set; }
    public float DefaultFOV { get; }
    public float AdditionalFOV { get; }


    public float ReachDistance { get; set; }
    public float DropItemStrength { get; set; }
}
