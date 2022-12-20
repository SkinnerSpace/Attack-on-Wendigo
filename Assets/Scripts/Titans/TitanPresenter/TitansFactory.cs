using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class TitansFactory
{
    public static Titan CreateTitan(Titans titan)
    {
        switch (titan)
        {
            case Titans.GINGERBREAD:
                return new Gingerbread();

            default:
                return null;
        }
    }
}
