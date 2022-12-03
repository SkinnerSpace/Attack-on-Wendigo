using System;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 20f;
    [SerializeField] public float jumpStrength = 30f;
    [SerializeField] public float gravityForce = 60f;
    [SerializeField] public float momentumDeceleration = 3f;
    [SerializeField] public float mouseSensitivity = 1f;

    [NonSerialized] public Vector3 velocity;
    [NonSerialized] public Vector3 velocityMomentum;
    [NonSerialized] public float verticalVelocity = 0f;
    [NonSerialized] public float momentumMultiplier = 7f;
}