using UnityEngine;

public class MovementController : IMovementController
{
    private ICharacterData data;
    private IChronos chronos;
    public MovementController(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Move(Vector3 inDirection)
    {
        Vector2 direction = ((inDirection.x * data.Right) + (inDirection.z * data.Forward)).FlatV2();
        Vector2 acceleration = direction * data.Speed * chronos.DeltaTime;
        data.FlatVelocity += acceleration;
    }
}

