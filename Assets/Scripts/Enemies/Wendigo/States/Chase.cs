using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Chase : IState
{
    public const float ROTATION_THRESHOLD = 1f;

    private WendigoRotator rotator;
    private WendigoMover mover;
    private readonly WendigoTarget target;

    public Chase(WendigoRotator rotator, WendigoMover mover, WendigoTarget target)
    {
        this.rotator = rotator;
        this.mover = mover;
        this.target = target;
    }

    public void Tick()
    {
        mover.MoveForward();
        if (ShouldRotate()) rotator.RotateToTarget(target.Position);
    }

    private bool ShouldRotate() => (target.Exist) && (mover.velocityMagn >= ROTATION_THRESHOLD);

    public void OnEnter() { }

    public void OnExit() { }
}
