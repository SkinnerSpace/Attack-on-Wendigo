using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WalkToTarget : IState
{
    private readonly IMover movementController;
    private readonly IRotator directionController;

    public WalkToTarget(IMover movementController, IRotator directionController)
    {
        this.movementController = movementController;
        this.directionController = directionController;
    }

    public void Tick()
    {
        //movementController.Move();
        //directionController.RotateTo();
    }

    public void OnEnter() { }

    public void OnExit() { }
}
