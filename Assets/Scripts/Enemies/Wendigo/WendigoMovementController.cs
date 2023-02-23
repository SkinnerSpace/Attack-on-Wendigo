using System;
using UnityEngine;

public class WendigoMovementController 
{
    private Wendigo wendigo;
    private WendigoData data;
    private IChronos chronos;

    public Action<float> onVelocityUpdate;

    public WendigoMovementController(Wendigo wendigo)
    {
        this.wendigo = wendigo;
        data = wendigo.Data;
        chronos = wendigo.Chronos;
    }

    public void Subscribe(IWendigoMovementObserver observer) => onVelocityUpdate += observer.OnVelocityUpdate;

    public void MoveForward()
    {
        Accelerate();
        Decelerate();

        onVelocityUpdate?.Invoke(data.Velocity.magnitude);
    }

    private void Accelerate()
    {
        Vector3 acceleration = data.Forward * data.MovementSpeed * chronos.DeltaTime;
        data.Velocity += acceleration;
    }

    private void Decelerate()
    {
        data.Velocity = Vector3.Lerp(data.Velocity, Vector3.zero, data.Deceleration * chronos.DeltaTime);
    }
}
