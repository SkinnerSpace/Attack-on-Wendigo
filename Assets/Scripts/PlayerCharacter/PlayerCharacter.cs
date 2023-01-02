using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private CapsuleCollider collisionBox;

    public float Speed => speed;
    [SerializeField] private float speed = 10f;
    public float JumpHeight => jumpHeight;
    [SerializeField] private float jumpHeight = 10f;
    public float Gravity => gravity;
    [SerializeField] private float gravity = 16f;

    public float height => collisionBox.height;

    private void Awake()
    {
        collisionBox = GetComponentInChildren<CapsuleCollider>();
    }
}
