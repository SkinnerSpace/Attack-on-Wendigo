using UnityEngine;

public class JumpController
{
    public ICharacterData data { get; private set; }
    public JumpController(ICharacterData data) => this.data = data;

    public void ResetJumpCount() => data.JumpCount = 0;

    public void Jump()
    {
        if (data.JumpCount < data.MaxJumpCount)
        {
            data.JumpCount += 1;
            data.VerticalVelocity = GetJumpVelocity() * data.JumpCount;
        }
    }

    private float GetJumpVelocity() => Mathf.Sqrt(data.JumpHeight * 2f * data.Gravity);
}
