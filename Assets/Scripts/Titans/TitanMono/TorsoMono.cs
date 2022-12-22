using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TorsoMono : MonoBehaviour
{
    private Torso torso;

    public void Initialize(TitanSetup titanSetup, Titan titan)
    {
        torso = new Torso(titanSetup, new TransformProxy(transform));
        titan.movementController.AddTorso(torso);
    }
}
