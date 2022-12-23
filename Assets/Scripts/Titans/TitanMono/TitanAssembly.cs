using System.Collections.Generic;
using UnityEngine;

public static class TitanAssembly
{
    public static Titan CreateTitan(TitanSetup setup, Transform transform)
    {
        Titan titan = TitansFactory.CreateTitan(setup.titan);
        TitanData data = new TitanData(setup);

        ITransformProxy transformProxy = new TransformProxy(transform);
        IMovementController movementController = new MovementController(transformProxy, titan.clock);
        IDirectionController directionController = new DirectionController(transformProxy, titan.clock);
        ITargetPointer targetPointer = new TargetPointer(transformProxy);

        titan.SetData(data);
        titan.SetTransform(transformProxy);
        titan.SetMovementController(movementController);
        titan.SetDirectionController(directionController);
        titan.SetTargetPointer(targetPointer);

        return titan;
    }
}
