using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Prop : IProp, IDestructible
{
    public IPropData data { get; private set; }
    public List<ITransformProxy> transforms { get; private set; }
    public bool isCollapsing { get; protected set; }

    public void AddToTown()
    {
        Town.Instance.AddProp(data.Type, this);
    }
    public void SetData(IPropData data)
    {
        this.data = data;
    }

    public void AddTransform(ITransformProxy transform)
    {
        transforms.Add(transform);
    }

    public void SetTransforms(List<ITransformProxy> transforms)
    {
        this.transforms = transforms;
    }

    public abstract void Collapse(Vector3 impact);
    public abstract void UpdateCollapse();
}
