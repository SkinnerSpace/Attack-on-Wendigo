using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PropFactory
{
    public static Prop CreateProp(PropTypes type)
    {
        switch (type)
        {
            case PropTypes.BUILDING:
                return new Building();

            case PropTypes.TREE:
                return new TreeProp();

            default:
                return null;
        }
    }
}
