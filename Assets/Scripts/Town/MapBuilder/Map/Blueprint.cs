using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Blueprint
{
    public Mark[,] map;
    public int offset;
    public float scale;
    public Transform field;

    public Blueprint(Mark[,] map, int offset, float scale, Transform field)
    {
        this.map = map;
        this.offset = offset;
        this.scale = scale;
        this.field = field;
    }
}
