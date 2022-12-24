using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MovementController : IMovementController
{
    private Rotator rotator;
    private Mover mover;
    private LegsSync legs;
    private Torso torso;

    public MovementController(Rotator rotator, Mover mover, LegsSync legs, Torso torso)
    {
        this.rotator = rotator;
        this.mover = mover;
        this.legs = legs;
        this.torso = torso;
    }

    public void MoveTo(ITransformProxy target)
    {
        rotator.RotateTo(target.Position);
        mover.MoveForward();
        legs.Walk();
    }
}