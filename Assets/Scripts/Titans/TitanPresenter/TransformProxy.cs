﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TransformProxy : ITransformProxy
{
    private Transform transform;
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    public Vector3 Forward => transform.forward;
    public Vector3 Right => transform.right;

    public TransformProxy(Transform transform)
    {
        this.transform = transform;
    }
}
