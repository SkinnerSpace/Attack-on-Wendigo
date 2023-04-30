using UnityEngine;

public class HelicopterEngine : MonoBehaviour
{
    [SerializeField] private Helicopter helicopter;
    [SerializeField] private HelicopterTimer synchronizer;
    [SerializeField] private HelicopterMover mover;
    [SerializeField] private HelicopterRotator rotator;
    [SerializeField] private HelicopterSway sway;
    [SerializeField] private DispenserManager dispenserManager;
    [SerializeField] private HelicopterData data;

    private HelicopterLogic logic;
    private bool movedForTheFirstTime;

    private void Awake(){
        logic = new HelicopterLogic(helicopter, data);
        SynchronizeComponents();

        data.targetRotation = data.rotation;
    }

    private void SynchronizeComponents()
    {
        synchronizer.onTimeUpdate += mover.UpdateCompletion;
        synchronizer.onTimeUpdate += rotator.UpdateCompletion;
        synchronizer.onTimeUpdate += sway.UpdateCompletion;
    }

    public void MoveAlongTheTrajectoryIfPossible(BezierTrajectory trajectory)
    {
        if (data.isMoving && GameState.PauseMode == PauseMode.None)
        {
            if (!data.skipFrame)
            {
                MoveAlongTheTrajectory(trajectory);
            }
            else
            {
                data.skipFrame = false;
            }
        }

        data.rotation = Quaternion.Lerp(data.rotation, data.targetRotation, data.rotationSpeed * Time.deltaTime);
    }

    private void MoveAlongTheTrajectory(BezierTrajectory trajectory)
    {
        synchronizer.UpdateTime();
        data.position = mover.Move(trajectory, ActOnArrival);
        data.targetRotation = rotator.Rotate(data.targetRotation, data.position, data.previousPosition);

        data.previousPosition = data.position;

        if (!movedForTheFirstTime){
            movedForTheFirstTime = true;
            HelicopterEvents.current.NotifyOnMovedForTheFirstTime();
        }
    }

    private void ActOnArrival()
    {
        data.isMoving = false;
        float idleTime = GetIdleTime();

        dispenserManager.DropCargoIfPossible(OnDispenserIsDone, idleTime);
    }

    private float GetIdleTime(){
        switch (data.state)
        {
            case HelicopterStates.IsGoingToLand:
                return data.landIdleTime;

            case HelicopterStates.Land:
                return data.descendIdleTime;

            default:
                return data.emptyStorageIdleTime;
        }
    }

    private void OnDispenserIsDone(){
        logic.MakeADecision();
    }
}
