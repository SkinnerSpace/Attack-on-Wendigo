using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct LegSetupPack
{
    public Sides side;
    public ITransformProxy transform;
    public ITransformProxy titanTransform;

    public float speed;
    public float stepDistance;
    public float spacing;
    public float stepHeight;
}
