using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Torso : ITorso
{
    private ITorsoController torsoController;

    public void SetTorsoController(ITorsoController torsoController)
    {
        this.torsoController = torsoController;
    }

    public void Update()
    {
        
    }
}
