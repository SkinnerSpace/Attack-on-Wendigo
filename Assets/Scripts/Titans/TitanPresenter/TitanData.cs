using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TitanData
{
    public float speed;
    public Titans titan;
    public TransformProxy transform;

    public TitanData()
    {

    }

    public TitanData(TitanSetup setup)
    {
        speed = setup.speed;
        titan = setup.titan;
    }
}
