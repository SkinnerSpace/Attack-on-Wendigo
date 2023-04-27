using System;
using System.Collections;
using UnityEngine;

public class WeaponSweeper : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private ItemPhysicalBody physics;
    [SerializeField] private WeaponPooledObject pooledObject;
    [SerializeField] private FunctionTimer timer;

    [SerializeField] private SweeperData data;


    private bool isSweeping;
    private bool isSwept;

    public void SweepTheWeapon()
    {
        if (!isSweeping)
        {
            isSweeping = true;
            StartCoroutine(WaitForRest());
        }
    }

    private IEnumerator WaitForRest()
    {
        while (physics.Velocity.magnitude > Mathf.Epsilon)
            yield return null;

        StartCoroutine(FallThrough());
    }

    private IEnumerator FallThrough()
    {
        PrepareForSweeping();

        while (!isSwept){
            MoveDown(); yield return null;
        }

        FinishSweeping();
    }

    private void PrepareForSweeping()
    {
        physics.DisablePhysics();
        timer.Set("OnSwept", data.sweepTime, () => isSwept = true);
    }

    private void MoveDown() => transform.position += Vector3.down * data.fallSpeed * Time.deltaTime;

    private void FinishSweeping()
    {
        isSwept = false;
        isSweeping = false;
        GameEvents.current.WeaponHasBeenSweptAway();
        pooledObject.BackToPool();
    }
}
