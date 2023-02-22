using UnityEngine;
using System;

public class CharacterMover : MonoBehaviour, IBaseController
{
    private CharacterData data;
    private Chronos chronos;

    private event Action onUpdate;
    private bool isReady;

    public void Initialize(MainController main)
    {
        data = main.Data;
        chronos = main.Chronos;
    }

    public void Connect() => isReady = true;

    public void Subscribe(IMoverObserver observer) => onUpdate += observer.Update;

    private void Update()
    {
        if (isReady)
        {
            data.CameraRotation = Quaternion.Euler(data.CameraViewEuler + data.CameraTiltEuler);
            onUpdate?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (isReady)
        {
            data.PreviousVerticalVelocity = data.VerticalVelocity;
            data.Velocity = new Vector3(data.FlatVelocity.x, data.VerticalVelocity, data.FlatVelocity.y);
            data.Controller.Move(data.Velocity * chronos.DeltaTime);
        }
    }
}

