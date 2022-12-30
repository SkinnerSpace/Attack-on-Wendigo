using System;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float walkSpeed = 10f;
    public float jumpStrength = 10f;
    public float gravityForce = 30f;

    [NonSerialized] public Vector3 velocity;
}
