using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WalkToTarget : IState
{
    private readonly IMovementController movementController;
    private readonly IDirectionController directionController;

    public WalkToTarget(IMovementController movementController, IDirectionController directionController)
    {
        this.movementController = movementController;
        this.directionController = directionController;
    }

    public void Tick()
    {
        movementController.Move();
        directionController.LookAt();
    }

    public void OnEnter() { }

    public void OnExit() { }
}
