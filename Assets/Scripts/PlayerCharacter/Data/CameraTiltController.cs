using UnityEngine;
using System;

public class CameraTiltController : BaseController, IMovementController
{
    private ICharacterData data;
    private IChronos chronos;
    private IInputReader input;

    public override void Initialize(MainController main)
    {
        data = main.Data;
        chronos = main.Chronos;
        input = main.InputReader;
    }

    public override void Connect() => input.Get<MovementInputReader>().Subscribe(this);
    public override void Disconnect() => input.Get<MovementInputReader>().Unsubscribe(this);

    public void Move(Vector3 inDirection)
    {
        float tiltAngle = -1 * inDirection.x * data.TiltMaxAngle;
        data.CameraTiltEuler = Vector3.Lerp(data.CameraTiltEuler, new Vector3(0f, 0f, tiltAngle), data.TiltSpeed * chronos.DeltaTime);
    }
}

