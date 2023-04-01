using System;
using UnityEngine;

public class Helicopter : MonoBehaviour, ILaunchable
{
    [SerializeField] private BezierTrajectory trajectory;
    [SerializeField] private HelicopterMover mover;
    [SerializeField] private HelicopterRotator rotator;
    [SerializeField] private HelicopterSway sway;
    [SerializeField] private DispenserManager dispenserManager;
    [SerializeField] private DispenserStorage storage;

    [SerializeField] private HelicopterTimer synchronizer;
    [SerializeField] private HelicopterData data;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private Chronos chronos;

    public FunctionTimer Timer => timer;

    private bool isMoving;

    public float DistancePassed => distancePassed;
    public float RouteCompletion => (trajectory != null) ? Mathf.Round((distancePassed / trajectory.Length) * 100f) : 0f;
    public float timeToComplete => (trajectory != null) ? (trajectory.Length - distancePassed) / data.Speed : 0f;

    private float distancePassed;
    private Vector3 prevPos;

    private void Awake()
    {
        SynchronizeComponents();
    }

    private void SynchronizeComponents()
    {
        synchronizer.Subscribe(mover);
        synchronizer.Subscribe(rotator);
        synchronizer.Subscribe(sway);
    }

    private bool skipFrame;

    public void Launch()
    {
        isMoving = true;
        skipFrame = true;

        distancePassed = 0f;
        trajectory.GenerateTrajectory();
        synchronizer.Set(trajectory.Length, data.Speed);
    }

    public void Stop() => isMoving = false;

    private void Update() => Move();

    private Vector3 currentPos;
    private Quaternion currentRotation;
    private float updateSpeed = 10f;

    public void Move()
    {
        if (isMoving && chronos.IsTicking)
        {
            if (!skipFrame)
            {
                synchronizer.UpdateTime();
                transform.position = mover.Move(trajectory, Arrived);
                transform.rotation = rotator.Rotate(transform.rotation, transform.position, prevPos);
                /*                currentPos = mover.Move(trajectory, Arrived);
                                currentRotation = rotator.Rotate(currentRotation, currentPos, prevPos);*/
/*
                float difference = Vector3.Distance(currentPos, prevPos);
                Debug.Log(difference);*/

                prevPos = transform.position;
                //prevPos = currentPos;
            }
            else
            {
                skipFrame = false;
            }
        }

/*        transform.position = Vector3.Lerp(transform.position, currentPos, updateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation, updateSpeed * Time.deltaTime);*/
    }

    public void Arrived()
    {
        isMoving = false;
        dispenserManager.DropAnItem(Launch);
    }

    public void UpdateDistance(float distancePassed) => this.distancePassed = distancePassed;
}
