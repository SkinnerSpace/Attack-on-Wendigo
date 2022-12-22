using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TransformProxy : ITransformProxy
{
    private readonly Transform transform;
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public Vector3 LocalPosition
    {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }

    public Vector3 Angle
    {
        get { return transform.eulerAngles; }
        set { transform.eulerAngles = value; }
    }

    public Vector3 LocalAngle
    {
        get { return transform.localEulerAngles; }
        set { transform.localEulerAngles = value; }
    }

    public Quaternion Rotation
    {
        get { return transform.rotation; }
        set { transform.rotation = value; }
    }

    public Vector3 Forward => transform.forward;
    public Vector3 Right => transform.right;

    public TransformProxy(Transform transform)
    {
        this.transform = transform;
    }
}
