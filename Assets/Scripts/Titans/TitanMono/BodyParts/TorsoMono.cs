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
        torso = new Torso(new TransformProxy(transform));
        torso.SetPosAndAngleDeviations(titanSetup.torsoPosDeviation, titanSetup.torsoAngleDeviation);
        titan.movementController.AddTorso(torso);
    }
}
