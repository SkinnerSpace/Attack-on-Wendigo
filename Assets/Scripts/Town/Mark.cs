using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Mark
{
    public Types type { get; private set; }
    public int index { get; private set; }
    public Quaternion rotation = Quaternion.identity;
    public Vector3 scale = Vector3.one;

    public Mark() { }

    public Mark(Types type, int index)
    {
        this.type = type;
        this.index = index;
    }

    public bool IsEmpty()
    {
        return type == Types.NONE && index == 0;
    }
}
