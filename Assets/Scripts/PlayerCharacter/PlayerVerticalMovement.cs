using System;
using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    public static float velocityMagnitude { get; private set; }
    public static float landMagnitude { get; private set; }

    private const int GROUND = 1 << 8;
    private const float GROUND_CHECK_RADIUS = 0.1f;
    private Vector3 groundCheckOffset;

    private PlayerCharacter player;
    private CharacterController controller;

    private float verticalVelocity;
    private bool isGrounded;

    [SerializeField] private WeaponSwayController weaponSway;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        float offset = (player.height / 2f) + GROUND_CHECK_RADIUS;
        groundCheckOffset = new Vector3(0f, offset, 0f);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        ApplyMovement();

        bool wasGrounded = isGrounded;
        isGrounded = CheckIsGrounded();
        Landing(wasGrounded);

        velocityMagnitude = verticalVelocity / player.JumpHeight;
    }

    private void HandleMovement()
    {
        if (isGrounded)
        {
            verticalVelocity = InputReader.jump ? GetJumpVelocity() : 0f;
        }
        else if (!isGrounded)
        {
            verticalVelocity -= player.Gravity * Time.deltaTime;
        }  
    }

    private void Landing(bool wasGrounded)
    {
        if ((wasGrounded != isGrounded) && isGrounded)
            landMagnitude = 1f;

        landMagnitude = Mathf.Lerp(landMagnitude, 0f, Time.deltaTime);
    }

    private float GetJumpVelocity()
    {
        float jumpVelocity = Mathf.Sqrt(player.JumpHeight * 2f * player.Gravity);
        return jumpVelocity;
    }

    private void ApplyMovement()
    {
        controller.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    private bool CheckIsGrounded()
    {
        Vector3 checkPoint = transform.position - groundCheckOffset;
        return Physics.CheckSphere(checkPoint, GROUND_CHECK_RADIUS, GROUND);
    }
}
