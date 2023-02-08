using System;
using UnityEngine;

public class DispenserManager : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private GameObject crate;
    [SerializeField] private Transform target;
    [SerializeField] private DispenserData data;

    [Header("Dispensers")]
    [SerializeField] private Dispenser rightDispenser;
    [SerializeField] private Dispenser leftDispenser;

    private void Awake()
    {
        rightDispenser.SetData(data);
        leftDispenser.SetData(data);
    }

    public void DropAnItem(Action moveOn)
    {
        Dispenser dispenser = TargetOnTheRightSide() ? rightDispenser : leftDispenser;
        dispenser.Launch(crate, moveOn);
    }

    private bool TargetOnTheRightSide()
    {
        Vector3 right = transform.right;
        Vector3 dirToTarget = (target.position - transform.position).normalized;

        float dot = Vector3.Dot(right, dirToTarget);

        return dot > 0;
    }
}
