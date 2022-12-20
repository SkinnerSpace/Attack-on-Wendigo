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

    public void AddLeg(ILeg leg)
    {
        if (legs == null)
            legs = new List<ILeg>();

        if (legs.Count < 2)
        {
            legs.Add(leg);
            leg.SetSynchronizer(this);
        }
    }

    public int GetLegsCount()
    {
        return legs.Count;
    }

    private ILeg GetNextLeg()
    {
        currentLeg += 1;
        if (currentLeg > legs.Count - 1)
            currentLeg = 0;

        return legs[currentLeg];
    }

    public void SetActive(bool active)
    {
        if (active) GetNextLeg().MakeAStep();
    }

    public void StepIsOver()
    {
        GetNextLeg().MakeAStep();
    }
}