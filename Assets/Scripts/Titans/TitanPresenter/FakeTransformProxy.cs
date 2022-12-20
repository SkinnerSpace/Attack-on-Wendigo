using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FakeTransformProxy : ITransformProxy
{
    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    private Vector3 position;

    public Vector3 Forward => new Vector3(0f, 0f, 1f);

    public Vector3 Right => new Vector3(1f, 0f, 0f);

    public FakeTransformProxy(Vector3 position)
    {
        this.position = position;
    }
}
