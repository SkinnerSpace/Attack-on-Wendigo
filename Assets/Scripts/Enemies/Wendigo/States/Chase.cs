using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Chase : IState
{
    private WendigoRotator rotator;
    private WendigoMover mover;

    private Wendigo wendigo;
    //private Transform target;

    public Chase(WendigoRotator rotator, WendigoMover mover, Wendigo wendigo)
    {
        this.rotator = rotator;
        this.mover = mover;
        this.wendigo = wendigo;
        //this.target = target;
    }

    public void Tick()
    {
        if (wendigo.Target != null) rotator.RotateToTarget(wendigo.Target);
        mover.MoveForward();
    }

    public void OnEnter() { }

    public void OnExit() { }
}
