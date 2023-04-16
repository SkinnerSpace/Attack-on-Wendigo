using System;
using UnityEngine;

public class Helicopter : MonoBehaviour, ILaunchable
{
    private enum States
    {
        Flying,
        PreparingToLand,
        Landing,
        Landed
    }
    private States state;

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

    public event Action onLanded;
    public event Action onTakeOff;

    private void Awake()
    {
        SynchronizeComponents();
    }

    private void Start()
    {
        GameEvents.current.onVictory += PrepareToLand;
    }

    private void Update()
    {
        Fly();

/*        if (Input.GetKeyDown(KeyCode.Z))
        {
            PrepareToLand();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Launch();
        }*/
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
        state = States.Flying;
        sway.SetFlyingMagnitude();

        isMoving = true;
        skipFrame = true;

        distancePassed = 0f;
        trajectory.GenerateTrajectory();
        synchronizer.Set(trajectory.Length, data.Speed);

        onTakeOff?.Invoke();
    }

    public void Land()
    {
        state = States.Landing;
        sway.SetLandingMagnitude();

        isMoving = true;
        skipFrame = true;

        distancePassed = 0f;
        trajectory.GenerateLandingTrajectory(transform.position);

        synchronizer.Set(trajectory.Length, data.Speed);
    }

    public void Stop() => isMoving = false;

    private void Fly()
    {
        if (isMoving && chronos.IsTicking)
        {
            if (!skipFrame)
            {
                synchronizer.UpdateTime();
                transform.position = mover.Move(trajectory, Arrived);
                transform.rotation = rotator.Rotate(transform.rotation, transform.position, prevPos);

                prevPos = transform.position;
            }
            else
            {
                skipFrame = false;
            }
        }
    }

    private void Arrived()
    {
        isMoving = false;

        if (state == States.Flying){
            dispenserManager.DropAnItem(Launch);
        }
        else if (state == States.PreparingToLand){
            dispenserManager.DropAnItem(Land);
        }
        else if (state == States.Landing){
            state = States.Landed;
            onLanded?.Invoke();
        }
    }

    private void PrepareToLand(){
        state = States.PreparingToLand;
    }
}
