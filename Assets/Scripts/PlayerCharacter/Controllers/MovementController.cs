using UnityEngine;

public class MovementController : BaseController, IMovementController
{
    private ICharacterData data;
    private IChronos chronos;

    public override void Initialize(MainController main)
    {
        data = main.Data;
        chronos = main.Chronos;
    }

    public override void Connect() => MainInputReader.Get<MovementInputReader>().Subscribe(this);

    public void Move(Vector3 inDirection)
    {
        Vector2 direction = ((inDirection.x * data.Right) + (inDirection.z * data.Forward)).FlatV2();
        Vector2 acceleration = direction * data.Speed * chronos.DeltaTime;
        data.FlatVelocity += acceleration;
    }
}

