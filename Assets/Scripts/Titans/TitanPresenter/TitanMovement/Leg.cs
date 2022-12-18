using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Leg : ILeg
{
    private Sides side; 
    public float Side => (side == Sides.Left) ? (-1f) : (1f);

    private Vector3 oldStepPos = Vector3.zero;
    private Vector3 newStepPos = Vector3.zero;
    private Vector3 currentStepPos = Vector3.zero;

    private TransformProxy transform;
    private TitanData data;

    private LegsSync synchronizer;
    private LegRaycaster raycaster;
    private LegEngine engine;

    public Leg(LegSetupPack legSetupPack)
    {
        side = legSetupPack.side;
        transform = legSetupPack.transform;
        data = legSetupPack.data;

        raycaster = new LegRaycaster(this);
        engine = new LegEngine(this);
    }

    private void Update()
    {
        UpdatePosition();
    }

    public void SetSynchronizer(LegsSync synchronizer)
    {
        this.synchronizer = synchronizer;
    }

    public void MakeAStep()
    {
        oldStepPos = transform.Position;
        newStepPos = raycaster.GetNextStepPos(data.transform);
        engine.SetActive(true);
    }

    private void UpdatePosition()
    {
        currentStepPos = engine.UpdatePosition(oldStepPos, newStepPos, data.speed * 0.2f);
        transform.position = currentStepPos;
    }

    public void StepIsOver()
    {
        oldStepPos = transform.position;
        synchronizer.StepIsOver();
    }

    public void Stop()
    {
        engine.SetActive(false);
    }
}
