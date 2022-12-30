using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Titan
{
    public TitanData data { get; set; }
    public ITransformProxy transform { get; set; }
    public IMovementController movementController { get; set; }
    public ITargetPointer targetPointer { get; set; }
    public IClock clock { get; set; }
    public StateMachine stateMachine;

    public ITransformProxy Target { get; set; }

    public abstract void Update();

    /*
    public Titan()
    {
        clock = new Clock();
        stateMachine = new StateMachine();

        var idle = new Idle();
        var search = new SearchForTarget(this, targetPointer);
        var walk = new WalkToTarget(movementController, directionController);

        At(idle, search, HasNoTarget());
        At(search, walk, HasTarget());
        //At(walk, idle, TargetIsReached());
        At(walk, search, HasNoTarget());

        Func<bool> HasNoTarget() => () => Target == null;
        Func<bool> HasTarget() => () => Target != null;
        //Func<bool> TargetIsReached() => () => true;

        stateMachine.SetState(search);

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
    }
    */
}
