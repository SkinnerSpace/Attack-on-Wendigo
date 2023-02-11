using System;
using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    public const float maxVelocityMagnitude = 3f;
    public static float velocityMagnitude { get; private set; }
    
    private CharacterData data;

    private PlayerCharacter player;
    //private CharacterController controller;
    private PlayerGroundDetector groundDetector;
    //private JumpController jumpHandler;
    private PlayerDashHandler dashHandler;

    //private float verticalVelocity;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        //controller = GetComponent<CharacterController>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        //jumpHandler = GetComponent<JumpController>();
        dashHandler = GetComponent<PlayerDashHandler>();
    }

    private void Update()
    {
        //HandleMovement();

        //velocityMagnitude = verticalVelocity / player.JumpHeight;
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, -maxVelocityMagnitude, velocityMagnitude / 2f);
    }

    private void FixedUpdate()
    {
        groundDetector.UpdateDetection();
    }

    private void HandleMovement()
    {
        if (groundDetector.isGrounded)
        {
            data.VerticalVelocity = 0f;
        }
        else if (!groundDetector.isGrounded && !dashHandler.isSoaring)
        {
            data.VerticalVelocity -= player.Gravity * Time.deltaTime;


            //jumpHandler.PerformAirJump(this);
        }  
    }

/*    public void SetVelocity(float verticalVelocity)
    {
        this.verticalVelocity = verticalVelocity;
    }*/

/*    private void ApplyMovement()
    {
        player.verticalVelocity = new Vector3(0f, verticalVelocity, 0f);
        controller.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }*/
}
