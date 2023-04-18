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
    }

    public void DropAnItem(Action moveOn)
    {
        if (IsAbleToDrop())
        {
            Debug.Log("Is able to drop");
            Dispenser dispenser = TargetOnTheRightSide() ? rightDispenser : leftDispenser;
            GameObject crate = pooler.SpawnFromThePool("Crate");
            dispenser.Launch(crate, moveOn);
        }
        else
        {
            Debug.Log("Is active " + isActive);
            Debug.Log("Is empty " + storage.IsEmpty());
            timer.Set("MoveOn", 2f, moveOn);
        }
    }

    private bool IsAbleToDrop(){
        return isActive && 
               !storage.IsEmpty();
    }

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
