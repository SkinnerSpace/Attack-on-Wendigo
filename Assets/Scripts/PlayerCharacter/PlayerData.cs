using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private CapsuleCollider capsuleCollider;

    public const float GROUND_MULTIPLIER = 10f;
    public const float AIR_MULTIPLIER = 0.4f;

    public float height { get; private set; }

    [Header("Camera")]
    [SerializeField] public float mouseSensitivity;

    [Header("Movement")]
    [SerializeField] public float moveSpeed = 6f;
    [SerializeField] public float jumpForce = 5f;

    [Header("Drag")]
    [SerializeField] public float groundDrag = 6f;
    [SerializeField] public float airDrag = 2f;

    [Header("Ground Detection")]
    [SerializeField] public LayerMask groundMask;

    public bool isGrounded { get; set; }
    public float groundDistance { get; private set; } = 0.1f;

    private void Awake()
    {
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        height = capsuleCollider.height;
    }
}
