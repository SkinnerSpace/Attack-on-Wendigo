using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class TitansAssemblyFactory
{
    public static TitansAssembly Create(TitanTypes type)
    {
        switch (type)
        {
            case TitanTypes.GINGERBREAD:
                return new GingerbreadAssembly();

            default:
                return null;
        }
    }
}
