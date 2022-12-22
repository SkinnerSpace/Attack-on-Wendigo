using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TitanMono : MonoBehaviour
{
    [SerializeField] private TitanSetup setup;
    [SerializeField] private List<LegMono> legs;
    [SerializeField] private TorsoMono torso;
    private Titan titan;

    private void Awake()
    {
        titan = TitanAssembly.CreateTitan(setup, transform);
        InitializeLegs();
        torso.Initialize(setup, titan);
    }

    private void InitializeLegs()
    {
        foreach (LegMono legMono in legs)
            legMono.Initialize(setup, titan);
    }

    private void Update()
    {
        titan.Update();
    }
}
