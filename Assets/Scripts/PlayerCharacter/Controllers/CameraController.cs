using UnityEngine;
using System.Collections;
using System;

public class CameraController : BaseController, IMouseMotionObserver
{
    private ICharacterData data;
    private IChronos chronos;
    
    private float xAngle;
    private float yAngle;

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

    public override void Initialize(MainController main)
    {
        data = main.Data;
        chronos = main.Chronos;
    }

    public override void Connect() => MainInputReader.Get<MouseMotionInputReader>().Subscribe(this);
}
