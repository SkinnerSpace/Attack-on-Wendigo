using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Mark
{
    public Types Type { get; private set; }
    public int Index { get; private set; }

    public Mark() { }

    public Mark(Types Type, int Index)
    {
        this.Type = Type;
        this.Index = Index;
    }

    public bool IsEmpty()
    {
        return Type == Types.NONE && Index == 0;
    }
}
