using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Titan
{
    public TitanData data;
    public MovementController movementController { get; private set; }

    public Titan(TitanData data)
    {
        this.data = data;
    }

    public abstract void Update();
    public void SetMovementController(MovementController movementController)
    {
        this.movementController = movementController;
    }
}
