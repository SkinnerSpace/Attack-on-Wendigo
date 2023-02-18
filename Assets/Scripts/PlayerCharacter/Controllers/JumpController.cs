using UnityEngine;

public class JumpController : BaseController, IGroundObserver
{
    private MainController main;
    private ICharacterData data;

    public override void Initialize(MainController main)
    {
        this.main = main;
        data = main.Data;
    }

    public void SetData(ICharacterData data) => this.data = data;

    public override void Connect()
    {
        main.GetController<GroundDetector>().Subscribe(this);
        MainInputReader.Get<JumpInputReader>().Subscribe(this);
    }

    public void TryToJump()
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


