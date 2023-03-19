using System;
using System.Collections;
using UnityEngine;

public class WeaponSweeper : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private WeaponPhysics physics;
    [SerializeField] private WeaponPooledObject pooledObject;
    [SerializeField] private FunctionTimer timer;

    [Header("Settings")]
    [SerializeField] private float fallSpeed = 0.5f;
    [SerializeField] private float sweepTime = 3f;

    private bool isSwept;

    public static event Action onSweptAway;
    public static void SubscribeOnSweptAway(Action onSwept) => onSweptAway += onSwept;

    public void SweepTheWeapon() => StartCoroutine(WaitForRest());

    private IEnumerator WaitForRest()
    {
        while (physics.Velocity.magnitude != 0f)
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
        timer.Set("OnSwept", sweepTime, () => isSwept = true);
    }

    private void MoveDown() => transform.position += Vector3.down * fallSpeed * Time.deltaTime;

    private void FinishSweeping()
    {
        isSwept = false;
        onSweptAway?.Invoke();
        pooledObject.BackToPool();
    }
}
