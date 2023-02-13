using UnityEngine;

public interface ICharacterData
{
    Vector3 Position { get; set; }
    Vector3 Forward { get; }
    Vector3 Right { get; }
    Vector3 Up { get; }

    float Height { get; }
    Vector3 Velocity { get; }
    Vector2 FlatVelocity { get; set; }
    float VerticalVelocity { get; set; }
    float Gravity { get; }
    float JumpHeight { get; }
    int JumpCount { get; set; }
    int MaxJumpCount { get; }
    bool IsJumping { get; set; }

    bool WasGrounded { get; set; }
    bool IsGrounded { get; set; }
    float GroundDetectionRadius { get; set; }
    float GroundDetectionHeight { get; set; }

    float Speed { get; }

    float Deceleration { get; set; }
    float GroundDeceleration { get; }
    float AirDeceleration { get; }

    float DashDistance { get; }
}