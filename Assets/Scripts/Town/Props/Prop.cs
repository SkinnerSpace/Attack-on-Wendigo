using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Prop : IProp
{
    public IPropData data { get; private set; }
    public ITransformProxy transform { get; private set; }

    public void AddToTown()
    {
        Town.Instance.AddProp(data.Type, this);
    }

    public void SetData(IPropData data)
    {
        this.data = data;
    }

    public void SetTransform(ITransformProxy transform)
    {
        this.transform = transform;
    }
}
