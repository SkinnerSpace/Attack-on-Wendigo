using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ProxyTransform : ITransform
{
    public Vector3 Position { get; set; }

    public Vector3 LocalPosition { get; set; }

    public Vector3 LocalScale { get; set; }


    public Vector3 Euler { get; set; }

    public Vector3 LocalEuler { get; set; }

    public Quaternion Rotation { get; set; }


    public Vector3 Right => transform.right;
    public Vector3 Up => transform.up;
    public Vector3 Forward => transform.forward;


    private readonly Transform transform;
    public ProxyTransform(Transform transform) => this.transform = transform;
}
