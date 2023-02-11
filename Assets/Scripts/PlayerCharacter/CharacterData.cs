using System;
using UnityEngine;

[Serializable]
public class CharacterData : MonoBehaviour, ICharacterData
{
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private int maxJumpCount;

    public Vector3 Velocity => new Vector3(flatVelocity.x, verticalVelocity, flatVelocity.y);

    public Vector2 FlatVelocity { get { return flatVelocity; } set { flatVelocity = value; } }
    private Vector2 flatVelocity;

    public float VerticalVelocity { get { return verticalVelocity; } set { verticalVelocity = value; } }
    private float verticalVelocity;

    public float Gravity => gravity;
    public float JumpHeight => jumpHeight;
    public int JumpCount { get; set; }
    public int MaxJumpCount => maxJumpCount;
}
