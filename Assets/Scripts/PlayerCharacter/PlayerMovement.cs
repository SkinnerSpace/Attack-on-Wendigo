using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveDirection;
    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 slopeMoveDirection;
    private RaycastHit slopeHit;

    private PlayerData data;
    private Rigidbody body;
    private PlayerKeybinds keys;
    [SerializeField] private Transform orientation;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        keys = GetComponent<PlayerKeybinds>();
        data = GetComponent<PlayerData>();

        body.freezeRotation = true;
    }

    private void Update()
    {
        CheckIsGrounded();
        ReadInput();
        SetDrag();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MoveBody();
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, data.height / 2f + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void CheckIsGrounded()
    {
        float halfBody = (data.height / 2f);
        data.isGrounded = Physics.CheckSphere(transform.position - new Vector3(0f, halfBody, 0f), data.groundDistance, data.groundMask);
    }

    private void ReadInput()
    {
        float moveRight = Input.GetKey(keys.moveRight) ? 1f : 0;
        float moveLeft = Input.GetKey(keys.moveLeft) ? -1f : 0;
        horizontalMovement = moveRight + moveLeft;

        float moveForwad = Input.GetKey(keys.moveForwad) ? 1f : 0;
        float moveBackward = Input.GetKey(keys.moveBackward) ? -1f : 0;
        verticalMovement = moveForwad + moveBackward;

        Vector3 rawMoveDirection = (orientation.forward * verticalMovement) + (orientation.right * horizontalMovement);
        moveDirection = rawMoveDirection.normalized;
    }

    private void SetDrag()
    {
        body.drag = data.isGrounded ? data.groundDrag : data.airDrag;
    }

    private void MoveBody()
    {
        float multiplier = data.isGrounded ? PlayerData.GROUND_MULTIPLIER : PlayerData.AIR_MULTIPLIER;
        Vector3 direction = OnSlope() ? slopeMoveDirection : moveDirection;
        
        body.AddForce(direction * data.moveSpeed * multiplier, ForceMode.Acceleration);
    }
}
