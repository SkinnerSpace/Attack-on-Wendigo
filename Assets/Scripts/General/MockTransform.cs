using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MockTransform : ITransform
{
    public Vector3 Position { get; set; }

    public Vector3 LocalPosition { get; set; }

    public Vector3 LocalScale { get; set; }


    public Vector3 Euler { get; set; }

    public Vector3 LocalEuler { get; set; }

    public Quaternion Rotation { get; set; }


    public Vector3 Right { get; set; } = Vector3.right;
    public Vector3 Up { get; set; } = Vector3.up;
    public Vector3 Forward { get; set; } = Vector3.forward;
}
