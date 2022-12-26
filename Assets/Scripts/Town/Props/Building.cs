using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Building : Prop, IBuilding
{
    public override void Collapse(Vector3 impact)
    {
        //PlayDestructionAnimation();
        //EmitParticles();
        //Earthquake(height);
    }

    public override void UpdateCollapse()
    {
        
    }
}
