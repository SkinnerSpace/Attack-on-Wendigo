using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class TitansAssembly
{
    public abstract Titan Assemble();
    public abstract void CreateCoreComponents(TitanSetup setup, Transform transform);
    public abstract void CreateMovementController(List<ILeg> legs, Torso torso);
    public abstract void SetCoreComponents(TitanData data, ITransformProxy transformProxy);
}
