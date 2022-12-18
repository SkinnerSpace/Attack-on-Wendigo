using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LegMono : MonoBehaviour
{
    [SerializeField] private TitanMono titan;
    [SerializeField] private Sides side;
    private Leg leg;

    private void Awake()
    {
        //LegSetupPack setupPack = CreateSetupPack();
        //leg = new Leg(setupPack);
    }

    private LegSetupPack CreateSetupPack()
    {
        LegSetupPack setupPack = new LegSetupPack();

        setupPack.side = side;
        setupPack.transform = new TransformProxy(transform);
        //setupPack.data = titan.data;

        return setupPack;
    }
}