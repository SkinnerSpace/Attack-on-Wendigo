using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Chase : LoggableState, IState
{
    public const float ROTATION_THRESHOLD = 1f;

    private WendigoData data;
    private WendigoRotationController rotationController;
    private WendigoMovementController movementController;

    public Chase(Wendigo wendigo)
    {
        data = wendigo.Data;
        rotationController = wendigo.GetController<WendigoRotationController>();
        movementController = wendigo.GetController<WendigoMovementController>();
    }

    public void Tick()
    {
        movementController.MoveForward();
        if (ShouldRotate()) rotationController.RotateToTarget(data.Target.position);
    }

    private bool ShouldRotate()
    {
        return data.Velocity.magnitude > 1f;
    }

    public void OnEnter()
    {
        LogEnter();
    }

    public void OnExit()
    {
        LogExit();
    }
}
