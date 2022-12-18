using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Pivot : IPivot
{
    private Transform pivot;
    public Vector3 position; public Vector3 Position => position != Vector3.zero ? position : pivot.position;
    public Vector3 Forward => pivot.forward;
    public Vector3 Right => pivot.right;

    public Pivot(Transform pivot)
    {
        this.pivot = pivot;
    }
}
