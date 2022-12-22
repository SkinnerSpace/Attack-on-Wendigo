﻿using UnityEngine;

public class Leg : ILeg
{
    private readonly Sides side; public float Side => (side == Sides.Left) ? (-1f) : (1f);

    public ITransformProxy Transform { get; private set; }
    public StepPos StepPos { get; private set; }
    public Foot Foot { get; private set; }

    public ILegsSync Synchronizer { get; private set; }
    public ILegRaycaster Raycaster { get; private set; }
    public ILegEngine Engine { get; private set; }

    public bool NextStepIsChosen { get; set; }

    public Leg(Sides side, ITransformProxy Transform)
    {
        this.side = side;
        this.Transform = Transform;

        StepPos = new StepPos();
        Foot = new Foot(Side);
    }

    public void SetRaycaster(ILegRaycaster Raycaster)
    {
        this.Raycaster = Raycaster;
    }

    public void SetEngine(ILegEngine Engine)
    {
        this.Engine = Engine;
    }

    public void SetSynchronizer(ILegsSync Synchronizer)
    {
        this.Synchronizer = Synchronizer;
    }

    public void Update()
    {
        StepPos.current = (NextStepIsChosen) ? Engine.UpdatePosition(StepPos.old, StepPos.novel) : StepPos.old;
        Transform.Position = StepPos.current;

        Foot.Update(Transform);
    }

    public void SetNextStep()
    {
        if (!NextStepIsChosen)
        {
            NextStepIsChosen = true;

            StepPos.old = Transform.Position;
            StepPos.novel = Raycaster.GetNextStepPosition();
        }
    }

    public void StepIsOver()
    {
        NextStepIsChosen = false;

        StepPos.old = Transform.Position;
        Synchronizer.StepIsOver();
    }
}
