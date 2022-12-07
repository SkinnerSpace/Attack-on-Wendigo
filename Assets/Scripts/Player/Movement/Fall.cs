using UnityEngine;

public class Fall : VerticalMovement
{
    public override void Execute()
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
