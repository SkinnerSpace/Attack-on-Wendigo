using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Blueprint
{
    public Mark[,] map;
    public float scale;
    public Transform field;
    public Vector3 offset;

    public Blueprint(Mark[,] map, float scale, Transform field)
    {
        this.map = map;
        this.scale = scale;
        this.field = field;
    }
}
