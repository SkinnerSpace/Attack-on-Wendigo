using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Chase : IState
{
    public const float ROTATION_THRESHOLD = 1f;

    private WendigoData data;
    private WendigoRotationController rotationController;
    private WendigoMovementController movementController;

    public Chase(Wendigo wendigo)
    {
        data = wendigo.Data;
        rotationController = wendigo.RotationController;
        movementController = wendigo.MovementController;
    }

    public void Tick()
    {
        movementController.MoveForward();
        if (ShouldRotate()) rotationController.RotateToTarget(data.Target.position);
    }

    private bool ShouldRotate() => false;

    public void OnEnter() { }

    public void OnExit() { }
}
