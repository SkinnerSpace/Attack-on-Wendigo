using UnityEngine;
using System;

public class VerticalMovement : MonoBehaviour, ICharacterDependee
{
    private Character character;

    public void SetUp(Character character)
    {
        this.character = character;
    }

    public Vector3 ApplyTo(Vector3 velocity)
    {
        Vector3 modifiedVelocity = velocity;

        if (character.body.isGrounded)
            character.data.verticalVelocity = character.data.jumpStrength * Convert.ToInt32(character.controller.GetJump());

        character.data.verticalVelocity -= character.data.gravityForce * Time.deltaTime;
        modifiedVelocity.y = character.data.verticalVelocity;

        return modifiedVelocity;
    }

    public void ResetGravity()
    {
        character.data.verticalVelocity = 0f;
    }
}