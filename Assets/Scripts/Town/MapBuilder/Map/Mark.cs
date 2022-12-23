using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Mark
{
    public PropTypes Type { get; private set; }
    public int Index { get; private set; }

    public Mark() { }

    public Mark(PropTypes Type, int Index)
    {
        this.Type = Type;
        this.Index = Index;
    }

    public bool IsEmpty()
    {
        return Type == PropTypes.NONE && Index == 0;
    }
}
