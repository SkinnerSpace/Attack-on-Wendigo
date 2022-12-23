using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Titan
{
    public ITitanData data { get; private set; }
    public ITransformProxy transform { get; private set; }
    public IMovementController movementController { get; private set; }
    public IDirectionController directionController { get; private set; }
    public ITargetPointer targetPointer { get; private set; }
    public IClock clock { get; private set; }
    public StateMachine stateMachine;

    public Object Target { get; set; }

    public abstract void Update();
    public abstract void Rotate();
    public abstract void MoveForward();
    public abstract void LookForTarget();

    public Titan()
    {
        clock = new Clock();
        stateMachine = new StateMachine();

        var idle = new Idle();
        var search = new SearchForTarget(this, targetPointer);
        var walk = new WalkToTarget(movementController, directionController);

        At(idle, search, HasNoTarget());
        At(search, walk, HasTarget());
        At(walk, idle, TargetIsReached());
        At(walk, search, HasNoTarget());

        Func<bool> HasNoTarget() => () => Target == null;
        Func<bool> HasTarget() => () => Target != null;
        Func<bool> TargetIsReached() => () => true;

        stateMachine.SetState(search);

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
    }

    public void SetData(ITitanData data)
    {
        this.data = data;
    }

    public void SetTransform(ITransformProxy transform)
    {
        this.transform = transform;
    }

    public void SetMovementController(IMovementController movementController)
    {
        this.movementController = movementController;
    }

    public void SetDirectionController(IDirectionController directionController)
    {
        this.directionController = directionController;
    }

    public void SetTargetPointer(ITargetPointer targetPointer)
    {
        this.targetPointer = targetPointer;
    }
}
