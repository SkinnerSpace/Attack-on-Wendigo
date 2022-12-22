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

    public Vector3 LocalPosition
    {
        get { return localPosition; }
        set { localPosition = value; }
    }
    private Vector3 localPosition;

    public Vector3 Angle
    {
        get { return angle; }
        set { angle = value; }
    }
    private Vector3 angle;

    public Vector3 LocalAngle
    {
        get { return localAngle; }
        set { localAngle = value; }
    }
    private Vector3 localAngle;

    public Quaternion Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }
    private Quaternion rotation;

    public Vector3 Forward => Vector3.forward;

    public Vector3 Right => Vector3.right;

    public FakeTransformProxy(Vector3 position)
    {
        this.position = position;
    }
}
