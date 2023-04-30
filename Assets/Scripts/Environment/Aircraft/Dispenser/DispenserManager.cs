using System;
using UnityEngine;

public class DispenserManager : MonoBehaviour, ISwitchable
{
    [Header("Required Components")]
    [SerializeField] private Transform target;
    [SerializeField] private DispenserData data;
    [SerializeField] private DispenserStorage storage;
    [SerializeField] private FunctionTimer timer;

    [Header("Dispensers")]
    [SerializeField] private Dispenser rightDispenser;
    [SerializeField] private Dispenser leftDispenser;

    private Dispenser dispenser;

    private IObjectPooler pooler;
    private bool isActive = true;

    private void Awake()
    {
        rightDispenser.SetData(data);
        leftDispenser.SetData(data);
    }

    private void Start()
    {
        pooler = PoolHolder.Instance;
        GameEvents.current.onVictory += SwitchOff;
        HelicopterEvents.current.onBoarded += JustOpenTheDoor;
        HelicopterEvents.current.onIsGoingToSetOff += JustShutTheDoor;
    }

    public void DropCargoIfPossible(Action moveOn, float idleTime)
    {
        if (IsAbleToDrop())
        {
            dispenser = GetDispencer();
            GameObject crate = pooler.SpawnFromThePool("Crate");
            dispenser.Launch(crate, moveOn);
        }
        else{
            WaitAndExecute(moveOn, idleTime);
        }
    }

    private void WaitAndExecute(Action moveOn, float idleTime)
    {
        if (idleTime > 0f){
            timer.Set("MoveOn", idleTime, moveOn);
        }
        else{
            moveOn();
        }
    }

    private void JustOpenTheDoor()
    {
        dispenser = GetDispencer();
        dispenser.JustOpenTheDoor();
    }

    private void JustShutTheDoor() => dispenser.JustShutTheDoor();

    private bool IsAbleToDrop(){
        return isActive && 
               !storage.IsEmpty();
    }

    private Dispenser GetDispencer() => TargetOnTheRightSide() ? rightDispenser : leftDispenser;

    private bool TargetOnTheRightSide()
    {
        Vector3 right = transform.right;
        Vector3 dirToTarget = (target.position - transform.position).normalized;

        float dot = Vector3.Dot(right, dirToTarget);

        return dot > 0;
    }

    public void SwitchOn()
    {
        isActive = true;
    }

    public void SwitchOff()
    {
        isActive = false;
    }
}
