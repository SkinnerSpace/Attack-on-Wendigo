using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BipedalController : RigController
{
    [SerializeField] private float footSpacing; public float FootSpacing => footSpacing;
    [SerializeField] private float stepDistance; public float StepDistance => stepDistance;
    [SerializeField] private float stepHeight; public float StepHeight => stepHeight;
    [SerializeField] private float speed; public float Speed => speed;
    [SerializeField] private LayerMask groundLayer; public LayerMask GroundLayer => groundLayer;

    private List<LegPoint> legs = new List<LegPoint>();
    private int currentLeg = 0;

    private void Update()
    {
        if (legs.Count > 0)
            legs[currentLeg].Walk();
    }

    public override void AddRigPoint(RigPoint rigPoint)
    {
        legs.Add(rigPoint as LegPoint);
    }

    public override void Notify(RigPoint rigPoint)
    {
        NextLeg();
    }

    public void NextLeg()
    {
        if ((currentLeg + 1) >= legs.Count)
        {
            currentLeg = 0;
        }
        else
        {
            currentLeg += 1;
        }
    }
}