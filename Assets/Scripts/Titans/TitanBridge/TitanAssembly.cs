using System.Collections.Generic;
using UnityEngine;

public static class TitanAssembly
{
    public static Titan CreateTitan(TitanSetup setup, Transform transform)
    {
        TitanData data = CreateData(setup);
        ITransformProxy transformProxy = new TransformProxy(transform);
        IMovementController movementController = new MovementController(transformProxy);

        Titan titan = TitansFactory.CreateTitan(setup.titan);
        titan.SetData(data);
        titan.SetTransform(transformProxy);
        titan.SetMovementController(movementController);

        return titan;
    }

    public static TitanData CreateData(TitanSetup setup)
    {
        TitanData data = new TitanData();
        data.name = setup.titanName;

        return data;
    }
}
