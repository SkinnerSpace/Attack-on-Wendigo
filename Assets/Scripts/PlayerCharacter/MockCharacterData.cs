using UnityEngine;

public class MockCharacterData : ICharacterData
{
    public Vector3 Velocity { get; private set; }
    public Vector2 FlatVelocity { get; set; }
    public float VerticalVelocity { get; set; }

    public float Gravity => 10f;

    public float JumpHeight => 10f;
    public int JumpCount { get; set; }
    public int MaxJumpCount => 2;
}
