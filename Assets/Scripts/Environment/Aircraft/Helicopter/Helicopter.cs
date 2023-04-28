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

    private bool isMoving;

    public float DistancePassed => distancePassed;
    public float RouteCompletion => (trajectory != null) ? Mathf.Round((distancePassed / trajectory.Length) * 100f) : 0f;
    public float timeToComplete => (trajectory != null) ? (trajectory.Length - distancePassed) / data.Speed : 0f;

    private float distancePassed;
    private Vector3 prevPos;
    private Quaternion targetRotation;

    private bool skipFrame;

    public event Action onLanded;
    public event Action onTakeOff;

    private void Awake()
    {
        SynchronizeComponents();
        targetRotation = transform.rotation;
    }

    private void Start(){
        GameEvents.current.onVictory += PrepareToLand;
    }

    private void Update(){
        Fly();
    }

    private void SynchronizeComponents()
    {
        synchronizer.onTimeUpdate += mover.UpdateCompletion;
        synchronizer.onTimeUpdate += rotator.UpdateCompletion;
        synchronizer.onTimeUpdate += sway.UpdateCompletion;
    }

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
                transform.position = mover.Move(trajectory, ActOnArrival);
                targetRotation = rotator.Rotate(targetRotation, transform.position, prevPos);
                //transform.rotation = rotator.Rotate(transform.rotation, transform.position, prevPos);

                prevPos = transform.position;
            }
            else
            {
                skipFrame = false;
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, data.RotationSpeed * Time.deltaTime);
    }

    private void ActOnArrival()
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
