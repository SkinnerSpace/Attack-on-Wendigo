using UnityEngine;
using System;

public class CameraTiltController : BaseController, IMovementController
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
        float tiltAngle = -1 * inDirection.x * data.TiltMaxAngle;
        data.CameraTiltEuler = Vector3.Lerp(data.CameraTiltEuler, new Vector3(0f, 0f, tiltAngle), data.TiltSpeed * chronos.DeltaTime);
    }
}

