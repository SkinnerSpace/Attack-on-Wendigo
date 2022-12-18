using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TransformProxy : ITransformProxy
{
    private Transform transform;
    public Vector3 position; public Vector3 Position => position != Vector3.zero ? position : transform.position;
    public Vector3 Forward => transform.forward;
    public Vector3 Right => transform.right;

    public TransformProxy()
    {

    }

    public TransformProxy(Vector3 position)
    {
        this.position = position;
    }

    public TransformProxy(Transform transform)
    {
        this.transform = transform;
    }
}
