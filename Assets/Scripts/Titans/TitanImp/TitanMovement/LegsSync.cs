using UnityEngine;
using System.Collections.Generic;

public class LegsSync : ILegsSync
{
    private const int MAX_LEGS_COUNT = 2;
    public List<ILeg> Legs { get; set; }

    public int Index { get; set; }
    public ILeg CurrentLeg { get; set; }

    public void AddLeg(ILeg leg)
    {
        if (Legs == null)
            Legs = new List<ILeg>();

        if (Legs.Count < MAX_LEGS_COUNT)
        {
            Legs.Add(leg);
            leg.SetSynchronizer(this);
        }
    }

    public int GetLegsCount()
    {
        return Legs.Count;
    }

    public void Update()
    {
        CurrentLeg = Legs[Index];
        CurrentLeg.SetNextStep();

        foreach (ILeg leg in Legs)
            leg.Update();
    }

    public void StepIsOver()
    {
        Index += 1;
        if (Index > Legs.Count - 1)
            Index = 0;
    }
}