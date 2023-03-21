using System;
using UnityEngine;

public class DispenserManager : MonoBehaviour
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

    private void Awake()
    {
        rightDispenser.SetData(data);
        leftDispenser.SetData(data);
    }

    private void Start()
    {
        pooler = PoolHolder.Instance;
    }

    public void DropAnItem(Action moveOn)
    {
        if (!storage.IsEmpty())
        {
            Dispenser dispenser = TargetOnTheRightSide() ? rightDispenser : leftDispenser;
            GameObject crate = pooler.SpawnFromThePool("Crate");
            dispenser.Launch(crate, moveOn);
        }
        else
        {
            timer.Set("MoveOn", 2f, moveOn);
        }
    }

    private bool TargetOnTheRightSide()
    {
        Vector3 right = transform.right;
        Vector3 dirToTarget = (target.position - transform.position).normalized;

        float dot = Vector3.Dot(right, dirToTarget);

        return dot > 0;
    }
}
