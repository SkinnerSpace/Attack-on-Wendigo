using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class TitansFactory
{
    public static Titan CreateTitan(TitanData data)
    {
        switch (data.titan)
        {
            case Titans.GINGERBREAD:
                return new Gingerbread(data);

            default:
                return null;
        }
    }
}
