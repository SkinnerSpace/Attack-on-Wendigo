using System;
using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour, IJumper
{
    public const float maxVelocityMagnitude = 3f;
    public static float velocityMagnitude { get; private set; }
    
    private PlayerCharacter player;
    private CharacterController controller;
    private PlayerGroundDetector groundDetector;
    private PlayerJumpHandler jumpHandler;
    private PlayerDashHandler dashHandler;

    private float verticalVelocity;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        jumpHandler = GetComponent<PlayerJumpHandler>();
        dashHandler = GetComponent<PlayerDashHandler>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        groundDetector.UpdateDetection();

        velocityMagnitude = verticalVelocity / player.JumpHeight;
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, -maxVelocityMagnitude, velocityMagnitude/2f);
    }

    private void HandleMovement()
    {
        if (groundDetector.isGrounded)
        {
            SetVelocity(0f);
            jumpHandler.ResetState();
            jumpHandler.PerformJump(this);
        }
        else if (!groundDetector.isGrounded && !dashHandler.isSoaring)
        {
            verticalVelocity -= player.Gravity * Time.deltaTime;
            jumpHandler.PerformAirJump(this);
        }  
    }

    public void SetVelocity(float verticalVelocity)
    {
        this.verticalVelocity = verticalVelocity;
    }

    private void ApplyMovement()
    {
        player.verticalVelocity = new Vector3(0f, verticalVelocity, 0f);
        controller.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }
}
