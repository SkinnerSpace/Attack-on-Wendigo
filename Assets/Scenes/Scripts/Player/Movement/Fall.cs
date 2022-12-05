using UnityEngine;

public class Fall : VerticalMovement, ICommand
{
    public void Execute()
    {
        float verticalVelocity = data.velocity.y;

        if (IsGrounded())
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity -= data.gravityForce * Time.deltaTime;
        }

        data.velocity = new Vector3(data.velocity.x, verticalVelocity, data.velocity.z);
    }
}
