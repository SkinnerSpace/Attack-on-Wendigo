using System.Collections.Generic;
using UnityEngine;

public static class TitanAssembly
{
    public static Titan CreateTitan(TitanSetup setup, Transform transform)
    {
        TitanData data = new TitanData(setup);
        ITransformProxy transformProxy = new TransformProxy(transform);
        IMovementController movementController = new MovementController(transformProxy);

        Titan titan = TitansFactory.CreateTitan(setup.titan);
        titan.SetData(data);
        titan.SetTransform(transformProxy);
        titan.SetMovementController(movementController);

        return titan;
    }
}
