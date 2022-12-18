using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LegsSync : ILegsSync
{
    public List<ILeg> legs { get; set; }
    private int currentLeg = 0;

    public LegsSync(List<ILeg> legs)
    {
        this.legs = legs;

        foreach (Leg leg in legs)
            leg.SetSynchronizer(this);
    }

    public void SetActive(bool active)
    {
        if (active) GetNextLeg().MakeAStep();
    }

    public void StepIsOver()
    {
        GetNextLeg().MakeAStep();
    }

    private ILeg GetNextLeg()
    {
        currentLeg += 1;
        if (currentLeg > legs.Count - 1)
            currentLeg = 0;

        return legs[currentLeg];
    }
}