using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{
    private ITransformProxy target;

    public override void Update() // => stateMachine.Tick();
    {
        stateMachine.Tick();

        /*
        if (target == null)
            LookForTarget();

        if (target != null)
            Rotate();

        MoveForward();
        */
    }

    public override void LookForTarget()
    {
        target = targetPointer.GetTarget(PropTypes.BUILDING);
    }

    public override void Rotate()
    {
        directionController.LookAt(target.Position, data.RotationSpeed);
    }

    public override void MoveForward()
    {
        movementController.Move(data.Speed);
    }
}
