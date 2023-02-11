using UnityEngine;

public interface ICharacterData
{
    Vector3 Velocity { get; }
    Vector2 FlatVelocity { get; set; }
    float VerticalVelocity { get; set; }
    float Gravity { get; }
    float JumpHeight { get; }
    int JumpCount { get; set; }
    int MaxJumpCount { get; }
}