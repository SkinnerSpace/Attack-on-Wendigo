using UnityEngine;

public class GravityController : IGroundObserver
{
    private ICharacterData data;
    private IChronos chronos;

    public GravityController(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void ApplyGravity()
    {
        if (!data.IsGrounded)
            data.VerticalVelocity -= data.Gravity * chronos.DeltaTime;
    }

    public void OnGrounded() => data.VerticalVelocity = 0f;
}
