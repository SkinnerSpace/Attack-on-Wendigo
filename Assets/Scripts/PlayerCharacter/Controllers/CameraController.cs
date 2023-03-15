using UnityEngine;
using System.Collections;
using System;

public class CameraController : BaseController, IMouseMotionObserver
{
    private ICharacterData data;
    private IChronos chronos;
    private IInputReader input;
    
    private float xAngle;
    private float yAngle;

    public override void Initialize(PlayerCharacter main)
    {
        data = main.Data;
        chronos = main.Chronos;
        input = main.InputReader;
    }

    public override void Connect() => input.Get<MouseMotionInputReader>().Subscribe(this);
    public override void Disconnect() => input.Get<MouseMotionInputReader>().Unsubscribe(this);

    public void ReceiveMotion(Vector2 motion)
    {
        xAngle += motion.x * chronos.DeltaTime;
        yAngle += motion.y * chronos.DeltaTime;
        yAngle = Mathf.Clamp(yAngle, -90f, 90f);

        UpdateCameraRotation();
        UpdateBodyRotation();
    }

    private void UpdateCameraRotation() => data.CameraViewEuler = new Vector3(yAngle, xAngle, data.CameraViewEuler.z);

    private void UpdateBodyRotation() => data.Euler = new Vector3(0f, xAngle, 0f);
}
