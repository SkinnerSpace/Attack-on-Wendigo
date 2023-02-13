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

    public float Speed { get; set; }
    public float DashDistance { get; set; }

    public float Deceleration { get; set; }
    public float GroundDeceleration { get; set; }
    public float AirDeceleration { get; set; }
}
