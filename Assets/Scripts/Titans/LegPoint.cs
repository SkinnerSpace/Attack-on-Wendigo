using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LegPoint : RigPoint, IDebugController
{
    public enum Side
    {
        Left = -1,
        Right = 1
    }

    [SerializeField] public Side side;

    private const float MAX_DISTANCE = 10f;
    private const float STEP_HEIGHT = 3f;
    private const float STEP_DISTANCE = 5f;
    private const float SPACING = 5f;
    private const int GROUND_LAYER = 1 << 8;
    private const float SPEED_ADJUSTMENT = 0.3f;

    [SerializeField] private Titan titan;
    private Transform pivot;
    [SerializeField] private BipedalController controller;

    private float speed;
    private Vector3 oldPos;
    private Vector3 currentPos;
    private float lerp;
    [SerializeField] private bool active;

    [SerializeField] private List<DebugSphere> nextDebug = new List<DebugSphere>();
    [SerializeField] private List<DebugSphere> exDebug = new List<DebugSphere>();
    [SerializeField] private List<DebugSphere> footDebug = new List<DebugSphere>();
    [SerializeField] private List<DebugSphere> pivotDebug = new List<DebugSphere>();
    [SerializeField] private List<DebugSphere> projectionDebug = new List<DebugSphere>();

    private Vector3 nextStepPos;

    private void Awake()
    {
        Initialize();
        SetDefaultPose();
        ApplyPose();
    }

    private void Initialize()
    {
        pivot = titan.transform;
        speed = titan.speed * SPEED_ADJUSTMENT;
        controller.AddLeg(this);
    }

    public void StepForward()
    {
        active = true;
        lerp = 0f;

        Ray nextStepRay = GetStepForwardRay();
        SetNextStepPos(nextStepRay);
    }

    private void Update()
    {
        
        Walk();

        ApplyPose();
    }

    private void LateUpdate()
    {
        UpdateDebug();
    }

    private void SetDefaultPose()
    {
        SetStandPose();
    }

    private void SetStandPose()
    {
        Ray ray = new Ray(GetStandPos(), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, GROUND_LAYER))
        {
            currentPos = raycastHit.point;
        }
    }

    private void ApplyPose()
    {
        transform.position = currentPos;
    }

    /*
     * GET DIRECTED STEP RAY -->
     */

    private Ray GetStepForwardRay()
    {
        Ray ray = new Ray(GetStepForwardPos(), Vector3.down);
        return ray;
    }

    private Vector3 GetStepForwardPos()
    {
        return (GetStandPos() + (pivot.forward * STEP_DISTANCE));
    }

    private Ray GetStepBackwardRay()
    {
        Ray ray = new Ray(GetStepBackwardPos(), Vector3.down);
        return ray;
    }

    private Vector3 GetStepBackwardPos()
    {
        return (GetStandPos() - (pivot.forward * STEP_DISTANCE));
    }

    /*
     * GET DIRECTED STEP RAY <--
     */

    private void SetNextStepPos(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, GROUND_LAYER))
        {
            oldPos = transform.position;
            nextStepPos = raycastHit.point;
            lerp = 0f;
        }
            //UpdateNextStepIfTooFar(raycastHit.point);
    }

    private void UpdateNextStepIfTooFar(Vector3 castStepPos)
    {
        if (Vector3.Distance(nextStepPos, castStepPos) > MAX_DISTANCE)
        {
            oldPos = transform.position;
            nextStepPos = castStepPos;
            lerp = 0f;
        }
    }

    private Vector3 GetStandPos()
    {
        return pivot.position + (pivot.right * (SPACING * (float)side));
    }

    private void Walk()
    {
        if (lerp < 1f)
        {
            if (active)
                IncrementLerp();
            MoveLeg();
        }
    }

    private void IncrementLerp()
    {
        lerp += speed * Time.deltaTime;
        if (lerp >= 1f)
        {
            lerp = 1f;
            active = false;
            controller.Stepped(this);
        }
    }

    private void MoveLeg()
    {
        Vector3 footPos = Vector3.Lerp(oldPos, nextStepPos, lerp);
        footPos.y = Mathf.Sin(lerp * Mathf.PI) * STEP_HEIGHT;
        currentPos = footPos;
    }

    public void UpdateDebug()
    {
        foreach (DebugSphere debugPoint in nextDebug)
            debugPoint.Draw(nextStepPos, Color.red);

        /*
        foreach (DebugSphere debugPoint in exDebug)
            debugPoint.Draw(exStepPos, Color.green);
        */

        /*
        foreach (DebugSphere debugPoint in footDebug)
            debugPoint.Draw(footPos, Color.blue);
        */

        foreach (DebugSphere debugPoint in pivotDebug)
            debugPoint.Draw(pivot.position, Color.yellow);
    }
}