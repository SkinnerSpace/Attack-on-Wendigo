using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BipedalController : MonoBehaviour
{
    private bool active;
    private List<LegPoint> legs = new List<LegPoint>();
    private int currentLeg = 0;

    [SerializeField] private Titan titan;

    public void AddLeg(LegPoint leg)
    {
        legs.Add(leg);
    }

    public void SetActive(bool active)
    {
        this.active = active;

        if (active == true)
        {
            legs[currentLeg].StepForward();
        }
    }

    public void Stepped (LegPoint leg)
    {
        if (active && leg == legs[currentLeg])
        {
            currentLeg += 1;
            if (currentLeg > legs.Count - 1)
                currentLeg = 0;

            legs[currentLeg].StepForward();
            titan.SetActive(active);
        }
    }
}