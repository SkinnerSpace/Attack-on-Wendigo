using UnityEngine;

public class DecelerationController
{
    private ICharacterData data;
    private IChronos chronos;

    public DecelerationController(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Decelerate()
    {
        SetDeceleration();
        ApplyDeceleration();
    }

    public void SetDeceleration() => data.Deceleration = data.IsGrounded ? data.GroundDeceleration : data.AirDeceleration;

    public void ApplyDeceleration() => data.FlatVelocity = Vector2.Lerp(data.FlatVelocity, Vector2.zero, data.Deceleration * chronos.DeltaTime);
}