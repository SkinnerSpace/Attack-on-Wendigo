using UnityEngine;
using System;

public class VerticalMovement : MonoBehaviour
{
    private CharacterData data;
    private CharacterController body;
    private IController controller;

    float verticalVelocity = 0f;

    private void Awake()
    {
        data = GetComponent<CharacterData>();
        body = GetComponent<CharacterController>();
        controller = GetComponent<IController>();
    }

    public Vector3 ApplyTo(Vector3 velocity)
    {
        Vector3 modifiedVelocity = velocity;

        if (body.isGrounded)
            verticalVelocity = data.jumpStrength * Convert.ToInt32(controller.GetJump());

        verticalVelocity += data.gravityForce * Time.deltaTime;
        modifiedVelocity.y = verticalVelocity;

        return modifiedVelocity;
    }

    public void ResetGravity()
    {
        verticalVelocity = 0f;
    }
}