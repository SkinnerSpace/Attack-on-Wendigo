using System;
using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour, IJumper
{
    public static float velocityMagnitude { get; private set; }
    
    private PlayerCharacter player;
    private CharacterController controller;
    private PlayerGroundDetector groundDetector;
    private PlayerJumpHandler jumpHandler;

    private float verticalVelocity;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        jumpHandler = GetComponent<PlayerJumpHandler>();
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
    }

    private void HandleMovement()
    {
        if (groundDetector.isGrounded)
        {
            SetVelocity(0f);
            jumpHandler.ResetState();
            jumpHandler.PerformJump(this);
        }
        else if (!groundDetector.isGrounded)
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
        controller.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }
}
