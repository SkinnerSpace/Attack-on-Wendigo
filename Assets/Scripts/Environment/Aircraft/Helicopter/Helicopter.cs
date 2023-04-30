using System;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private HelicopterEngine engine;
    [SerializeField] private HelicopterTimer synchronizer;
    [SerializeField] private HelicopterSway sway;
    [SerializeField] private HelicopterElevator elevator;

    [Header("Data")]
    [SerializeField] private HelicopterData data;
    [SerializeField] private BezierTrajectory trajectory;

    public event Action onLanded;
    public event Action onSetOff;

    private void Awake()
    {
        Fly();
    }

    private void Start(){
        GameEvents.current.onVictory += PrepareToLand;
    }

    private void Update(){
        engine.MoveAlongTheTrajectoryIfPossible(trajectory);
    }

    public void Fly()
    {
        if (IsNotGoingToLand()){
            data.state = HelicopterStates.Follow;

            StartMoving();

            trajectory.GenerateTrajectory();
            synchronizer.Set(trajectory.Length, data.speed);
            sway.SetFlyingMagnitude();
        }
        else
        {
            Land();
        }
    }

    private bool IsNotGoingToLand() => data.state != HelicopterStates.IsGoingToLand;

    private void PrepareToLand() => data.state = HelicopterStates.IsGoingToLand;

    public void Land(){
        data.state = HelicopterStates.Land;

        StartMoving();

        trajectory.GenerateLandingTrajectory(data.position);
        synchronizer.Set(trajectory.Length, data.speed);
        sway.SetLandingMagnitude();

        synchronizer.Set(trajectory.Length, data.speed);
    }

    public void Descend() => elevator.Descend();

    public void NotifyOnLanded() => onLanded?.Invoke();

    public void Escape()
    {
        data.state = HelicopterStates.Escape;

        StartMoving();

        trajectory.GenerateEscapeTrajectroy();
        synchronizer.Set(trajectory.Length, data.speed);
        sway.SetFlyingMagnitude();

        onSetOff?.Invoke();
    }

    private void StartMoving()
    {
        data.isMoving = true;
        data.skipFrame = true; // Don't ask why
    }
}
