using System;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private BezierTrajectory trajectory;
    [SerializeField] private HelicopterMover mover;
    [SerializeField] private HelicopterRotator rotator;
    [SerializeField] private HelicopterSway sway;
    [SerializeField] private DispenserManager dispenserManager;

    [SerializeField] private HelicopterTimer synchronizer;
    [SerializeField] private HelicopterData data;
    [SerializeField] private FunctionTimer timer;

    private bool isMoving;

    public float DistancePassed => distancePassed;
    public float RouteCompletion => (trajectory != null) ? Mathf.Round((distancePassed / trajectory.Length) * 100f) : 0f;
    public float timeToComplete => (trajectory != null) ? (trajectory.Length - distancePassed) / data.Speed : 0f;

    private float distancePassed;
    private Vector3 prevPos;

    [SerializeField] private bool needToDrop;

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

    public void Launch()
    {
        arrived = false;
        isMoving = true;

        distancePassed = 0f;
        trajectory.GenerateTrajectory();
        synchronizer.Set(trajectory.Length, data.Speed);
    }

    public void Stop() => isMoving = false;

    private void Update() => Move();

    public void Move()
    {
        if (isMoving)
        {
            synchronizer.UpdateTime();
            transform.position = mover.Move(trajectory, Arrived);
            transform.rotation = rotator.Rotate(transform.rotation, prevPos);

            prevPos = transform.position;
        }
    }

    private bool arrived;

    public void Arrived()
    {
        arrived = true;
        isMoving = false;

        if (needToDrop)
        {
            dispenserManager.DropAnItem(Launch);
        }
        else
        {
            Launch();
        }
    }
}
