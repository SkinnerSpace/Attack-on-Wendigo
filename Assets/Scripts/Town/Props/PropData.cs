using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PropData : IPropData
{
    public PropTypes Type { get; private set; }
    public Vector3 size; public Vector3 Size => size;

    public PropData(PropSetup setup)
    {
        Type = setup.type;
    }

    public void SetSize(Vector3 size)
    {
        this.size = size;
    }
}
