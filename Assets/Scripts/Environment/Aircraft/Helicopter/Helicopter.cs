using System;
using UnityEngine;

public class Helicopter : MonoBehaviour, ILaunchable
{
    private enum States
    {
        Flying,
        PreparingToLand,
        Landing,
        Descending,
        Landed,
        Escaping
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

    [Header("Descending")]
    [SerializeField] private float descendingTime;
    [SerializeField] private float descendingHeight;

    private float currentDescendingTime;
    private Vector3 positionBeforDescend;
    private Vector3 descendDestination;

    private bool isMoving;

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
        GameEvents.current.onHelicopterIsGoingToSetOff += Escape;
    }

    private void Update(){
        if (state != States.Descending){
            UpdatePositionAccordingToTheTrajectory();
        }
        else{
            UpdateDescending();
        }
    }

    private void SynchronizeComponents()
    {
        synchronizer.onTimeUpdate += mover.UpdateCompletion;
        synchronizer.onTimeUpdate += rotator.UpdateCompletion;
        synchronizer.onTimeUpdate += sway.UpdateCompletion;
    }

    public void Launch()
    {
        if (state != States.PreparingToLand){
            state = States.Flying;

            StartMoving();

            trajectory.GenerateTrajectory();
            synchronizer.Set(trajectory.Length, data.Speed);
            sway.SetFlyingMagnitude();

            onTakeOff?.Invoke();
        }
        else
        {
            Land();
        }
    }

    public void Land()
    {
        state = States.Landing;

        StartMoving();

        trajectory.GenerateLandingTrajectory(transform.position);
        synchronizer.Set(trajectory.Length, data.Speed);
        sway.SetLandingMagnitude();

        synchronizer.Set(trajectory.Length, data.Speed);
    }

    private void Descend()
    {
        state = States.Descending;

        //StartMoving();

        positionBeforDescend = transform.position;
        trajectory.GenerateDescendingTrajectory(descendingHeight);
        descendDestination = trajectory.GetEndPointPosition();

        //synchronizer.Set(trajectory.Length, data.Speed);

    }

    public void Escape()
    {
        state = States.Escaping;

        StartMoving();

        trajectory.GenerateEscapeTrajectroy();
        synchronizer.Set(trajectory.Length, data.Speed);
        sway.SetFlyingMagnitude();

        onTakeOff?.Invoke();
    }

    private void StartMoving()
    {
        isMoving = true;
        skipFrame = true;
    }

    public void Stop() => isMoving = false;

    private void UpdatePositionAccordingToTheTrajectory()
    {
        if (isMoving && chronos.IsTicking)
        {
            if (!skipFrame)
            {
                MoveAlongTheTrajectory();
            }
            else
            {
                skipFrame = false;
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, data.RotationSpeed * Time.deltaTime);
    }

    //private void

    private void MoveAlongTheTrajectory()
    {
        synchronizer.UpdateTime();
        transform.position = mover.Move(trajectory, ActOnArrival);
        targetRotation = rotator.Rotate(targetRotation, transform.position, prevPos);

        prevPos = transform.position;
    }

    private void ActOnArrival()
    {
        isMoving = false;
        dispenserManager.DropCargoIfPossible(OnDispenserIsDone);
    }

    private void OnDispenserIsDone()
    {
        if (state == States.Flying){
            Launch();
        }
        else if (state == States.PreparingToLand){
            Land();
        }
        else if (state == States.Landing)
        {
            Descend();
            //onLanded?.Invoke();
        }
    }

    private void PrepareToLand(){
        state = States.PreparingToLand;
    }

    private void UpdateDescending()
    {

    }
}