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

    public void SweepTheWeapon() => StartCoroutine(WaitForRest());

    private IEnumerator WaitForRest()
    {
        while (physics.Velocity.magnitude != 0f)
            yield return null;

        StartCoroutine(FallThrough());
    }

    private IEnumerator FallThrough()
    {
        physics.SetPhysicsDisabled(true);
        timer.Set("OnSwept", sweepTime, () => isSwept = true);

        while (!isSwept)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null;
        }

        isSwept = false;
        pooledObject.BackToPool();
    }
}
