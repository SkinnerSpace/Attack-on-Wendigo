using UnityEngine;

public class JumpController : IJumpController, IGroundObserver
{
    private ICharacterData data;
    public JumpController(ICharacterData data) => this.data = data;

    public void OnJump()
    {
        JumpOffTheGround();
        JumpInTheAir();
    }

    public void OnStop() => data.IsJumping = false;

    public void JumpOffTheGround()
    {
        if (data.IsGrounded && data.JumpCount == 0)
        {
            data.IsJumping = true;
            ApplyJumpForce();
        }
    }

    public void JumpInTheAir()
    {
        if (!data.IsJumping && (!data.IsGrounded && data.JumpCount > 0) && (data.JumpCount < data.MaxJumpCount))
        {
            data.IsJumping = true;
            ApplyJumpForce();
        }
    }

    public void ApplyJumpForce()
    {
        data.JumpCount += 1;
        data.VerticalVelocity = GetJumpVelocity() * data.JumpCount;
    }

    private float GetJumpVelocity() => Mathf.Sqrt(data.JumpHeight * 2f * data.Gravity);

    public void OnGrounded() => data.JumpCount = 0;
}


