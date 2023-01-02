using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerVerticalMovement : MonoBehaviour
{
    private const int GROUND = 1 << 8;
    private const float GROUND_CHECK_RADIUS = 0.1f;
    private Vector3 groundCheckOffset;

    private PlayerCharacter player;
    private CharacterController controller;
    private IKeyBinds keys;

    private float verticalVelocity;
    private bool isGrounded;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
        keys = GetComponent<IKeyBinds>();
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
        isGrounded = CheckIsGrounded();
    }

    private void HandleMovement()
    {
        if (isGrounded)
        {
            verticalVelocity = Input.GetKey(keys.Jump) ? GetJumpVelocity() : 0f;
        }
        else if (!isGrounded)
        {
            verticalVelocity -= player.Gravity * Time.deltaTime;
        }  
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
