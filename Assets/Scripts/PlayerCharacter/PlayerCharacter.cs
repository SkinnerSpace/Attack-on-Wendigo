using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private CapsuleCollider collisionBox;

    [Header("Required Components")]
    [SerializeField] private CharacterData data;
    [SerializeField] private PlayerHorizontalMover movementController;
    [SerializeField] private WeaponHandler weaponHandler;

    [Header("Input Readers")]
    [SerializeField] private MovementInputReader movementInputReader;
    [SerializeField] private CombatInputReader combatInputReader;

    [Header("Horizontal movement")]
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float dashDistance = 10f;

    public float Speed { get; set; }
    public float MinSpeed => minSpeed;
    public float MaxSpeed => maxSpeed;
    public float Acceleration => acceleration;
    public float Deceleration => deceleration;
    
    [Header("Vertical movement")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = 16f;

    public float JumpHeight => jumpHeight;
    public float DashDistance => dashDistance;
    public float Gravity => gravity;
    
    public float height => collisionBox.height;

    public bool IsGrounded => PlayerGroundDetector.Instance.isGrounded;

    public Vector3 position => transform.position;

    public Vector3 velocity => new Vector3(horizontalVelocity.x, verticalVelocity.y, horizontalVelocity.z);
    public Vector3 horizontalVelocity;
    public Vector3 verticalVelocity;

    public static PlayerCharacter Instance { get; private set; }

    private bool isActive;

    private void Update()
    {
        if (!isActive && Input.GetKeyDown(KeyCode.C))
        {
            isActive = true;
            movementInputReader.Subscribe(movementController);
            weaponHandler.SetCombatInputReader(combatInputReader);
        }
    }

    private void Awake()
    {
        Instance = this;
        Speed = MinSpeed;
    }
}
