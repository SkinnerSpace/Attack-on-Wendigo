using System;
using System.Collections;
using UnityEngine;

public class ItemSweeper : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private ItemPhysicalBody physics;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private SweeperData data;

    private IPooledObject pooledObject;

    private bool isSweeping;
    private bool isSwept;

    private void Awake()
    {
        pooledObject = GetComponent<IPooledObject>();
    }

    public void SweepTheWeapon()
    {
        if (!isSweeping){
            isSweeping = true;

            StartCoroutine(WaitForRest());
        }
    }

    private IEnumerator WaitForRest()
    {
        while (physics.Velocity.magnitude > 0.05f){
            yield return null;
        }

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
        pooledObject.BackToPool();
    }
}
