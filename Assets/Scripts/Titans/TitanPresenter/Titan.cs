using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Titan
{
    public TitanData data { get; private set; }
    public ITransformProxy transform { get; private set; }
    public IMovementController movementController { get; private set; }

    public abstract void Update();
    public abstract void Move();

    public void SetData(TitanData data)
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
}
