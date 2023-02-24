using UnityEngine;

public interface ICharacterData
{
    Vector3 Position { get; set; }
    Vector3 Forward { get; }
    Vector3 Right { get; }
    Vector3 Up { get; }
    Vector3 Euler { get; set; }
    float Height { get; }


    Camera Cam { get; }
    Vector3 CameraLocalPos { get; set; }
    Vector3 CameraDampedPos { get; set; }

    Vector3 CameraEuler { get; set; }
    Quaternion CameraRotation { get; set; }
    Vector3 CameraForward { get; }
    Vector3 CameraRight { get; }
    Vector3 CameraUp { get; }
    Vector3 CameraViewEuler { get; set; }
    Quaternion CameraViewRotation { get; set; }

    Vector3 CameraTiltEuler { get; set; }
    float TiltSpeed { get; }
    float TiltMaxAngle { get; }

    Vector3 ShakePosition { get; set; }
    Vector3 ShakeEuler { get; set; }



    Vector3 Velocity { get; }
    Vector2 FlatVelocity { get; set; }
    float VerticalVelocity { get; set; }
    float PreviousVerticalVelocity { get; }



    float Speed { get; }

    float Deceleration { get; set; }
    float GroundDeceleration { get; }
    float AirDeceleration { get; }




    float Gravity { get; }
    float JumpHeight { get; }
    int JumpCount { get; set; }
    int MaxJumpCount { get; }
    bool IsJumping { get; set; }


    bool IsGrounded { get; set; }
    bool WasGrounded { get; set; }
    float GroundDetectionRadius { get; set; }
    float GroundDetectionHeight { get; set; }


    IDampedSpringData DampedSpring { get; }


    bool IsAbleToDash { get; set; }
    Vector2 DashDirection { get; set; }
    float DashDistance { get; }
    float DashCoolDownTime { get; }


    float MinFOV { get; }
    float MaxFOV { get; }
    float FOV { get; set; }
    float FOVChangeSpeed { get; }
    float FOVPower { get; set; }


    float DropItemStrength { get; }
}
