using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Leg : RigPoint, IDebugController
{
    [SerializeField] private Sides side; 
    public float Side => (side == Sides.Left) ? (-1f) : (1f);

    private float speed = 0.3f;
    private Vector3 oldStepPos = Vector3.zero;
    private Vector3 newStepPos = Vector3.zero;
    private Vector3 currentStepPos = Vector3.zero;
    private Vector3 bodyPosition = Vector3.zero;

    [SerializeField] private Titan titan;
    private Transform pivot;

    [SerializeField] private List<DebugSphere> nextDebug = new List<DebugSphere>();
    [SerializeField] private List<DebugSphere> exDebug = new List<DebugSphere>();
    [SerializeField] private List<DebugSphere> footDebug = new List<DebugSphere>();
    [SerializeField] private List<DebugSphere> pivotDebug = new List<DebugSphere>();
    [SerializeField] private List<DebugSphere> projectionDebug = new List<DebugSphere>();

    private LegsSynchronizer synchronizer;
    private LegRaycaster raycaster;
    private LegEngine engine;

    private void Awake()
    {
        pivot = titan.transform;
        raycaster = new LegRaycaster(this);
        engine = new LegEngine(this);
    }

    public void SetSynchronizer(LegsSynchronizer synchronizer)
    {
        this.synchronizer = synchronizer;
    }

    public void MakeAStep()
    {
        Pivot pivotProxy = new Pivot(pivot);
        pivotProxy.position = synchronizer.bodyPosition;

        oldStepPos = transform.position;
        newStepPos = raycaster.GetNextStepPos(pivotProxy);
        engine.SetActive(true);
    }

    public void Stop()
    {
        engine.SetActive(false);
    }

    private void Update()
    {
        currentStepPos = engine.UpdatePosition(oldStepPos, newStepPos, speed);
        transform.position = currentStepPos;
        UpdateDebug();
    }

    public void StepIsOver()
    {
        oldStepPos = transform.position;
        synchronizer.StepIsOver(this);

        CalculateNewBodyPosition();
    }

    private void CalculateNewBodyPosition()
    {
        Vector3 pivotDir = pivot.forward;
        Vector3 relativePos = transform.position - pivot.position;
        bodyPosition = pivot.position + (pivotDir * relativePos.x) + (pivotDir * 3f);

        synchronizer.SetBodyPosition(bodyPosition);
    }

    public void UpdateDebug()
    {
        Debug.DrawRay(bodyPosition, new Vector3(0f, 100f, 0f), Color.red);
        //Debug.DrawRay(pivot.position + new Vector3(0f, 5f, 0f), relativeDir * relativeLength, Color.green);
        //Debug.DrawRay(pivot.position + (relativeDir * relativeLength), )
        //Debug.DrawRay(adjustedRelPos, new Vector3(0f, 50f, 0f), Color.cyan);
        //Debug.DrawRay(pivot.position + new Vector3(0f, 6f, 0f), pivotDir * relativeLength, Color.blue);

        foreach (DebugSphere debugPoint in nextDebug)
            debugPoint.Draw(currentStepPos, Color.red);

        foreach (DebugSphere debugPoint in pivotDebug)
            debugPoint.Draw(pivot.position, Color.yellow);
    }
}
