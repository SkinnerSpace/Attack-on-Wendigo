using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TitanData
{
    public string name;
    public float speed;
    public Titans titan;
    //public ITransformProxy transform { get; private set; }

    public TitanData()
    {

    }

    public TitanData(TitanSetup setup)
    {
        speed = setup.speed;
        titan = setup.titan;
    }

    /*
    public void SetTransform(ITransformProxy transform)
    {
        //this.transform = transform;
    }
    */
}
