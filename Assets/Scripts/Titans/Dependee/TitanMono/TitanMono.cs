using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TitanMono : MonoBehaviour
{
    [SerializeField] private TitanSetup setup;
    [SerializeField] private List<LegMono> legsMono;
    [SerializeField] private TorsoMono torsoMono;
    private Titan titan;

    private void Awake()
    {
        TitansAssembly titansAssembly = TitansAssemblyFactory.Create(setup.titanType);

        titansAssembly.CreateCoreComponents(setup, transform);
        titansAssembly.CreateMovementController(GetLegs(), GetTorso());
        titan = titansAssembly.Assemble();
    }

    public List<ILeg> GetLegs()
    {
        List<ILeg> legs = new List<ILeg>();
        foreach (LegMono legMono in legsMono)
            legs.Add(legMono.GetLeg());

        return legs;
    }

    private Torso GetTorso()
    {
        return torsoMono.GetTorso();
    }

    private void Update()
    {
        titan.Update();
    }
}
