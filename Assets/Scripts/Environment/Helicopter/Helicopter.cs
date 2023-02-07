using System;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    private enum States
    {
        Idle,
        Move,
        Rotate
    }

    private States state;

    [SerializeField] private BezierTrajectory trajectory;
    [SerializeField] private HelicopterMover mover;
    [SerializeField] private HelicopterRotator rotator;

    [SerializeField] private HelicopterTimer synchronizer;
    [SerializeField] private HelicopterData data;

    private FunctionTimer timer;
    private float idleTime = 2f;

    public float DistancePassed => distancePassed;
    public float RouteCompletion => Mathf.Round((distancePassed / trajectory.Length) * 100f);
    public float timeToComplete => (trajectory.Length - distancePassed) / data.Speed;

    private float rotationTime;
    private float currentRotationTime;

    private float distancePassed;

    private Action onFinish;

    private Vector3 prevPos;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();

        onFinish += Arrived;
        synchronizer.Subscribe(mover);
    }

    public void SetOff()
    {
        trajectory.GenerateTrajectory();
        Launch();
    }

    public void Launch()
    {
        state = States.Move;
        synchronizer.Set(trajectory.Length, data.Speed);
    }

    private void Update() => UpdateState();

    public void Stop() => state = States.Idle;

    public void UpdateState()
    {
        switch (state)
        {
            case States.Move:
                synchronizer.UpdateTime();
                transform.position = mover.Move(trajectory, onFinish);
                transform.rotation = rotator.Rotate(prevPos);
                SavePreviousPosition();
                break;

            case States.Rotate:
                break;
        }
    }
    
    private void SavePreviousPosition()
    {
        prevPos = transform.position;
    }

    public void Arrived()
    {
        state = States.Idle;
        distancePassed = 0f;

        timer.Set("SetOff", idleTime, SetOff);
    }
}
