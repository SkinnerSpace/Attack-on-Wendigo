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

    private IObjectPooler pooler;
    private ParticleSystem sweepParticle;

    private bool isSweeping;
    private bool isSwept;

    private void Start()
    {
        pooler = PoolHolder.Instance;
    }

    public void SweepTheWeapon()
    {
        if (!isSweeping)
        {
            isSweeping = true;
            Vector3 sweepPosition = transform.position;

            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Landscape))
            {
                sweepPosition = hit.point;
            }

            sweepParticle = pooler.SpawnFromThePool("SweepSnowParticle", sweepPosition, Quaternion.identity).GetComponent<ParticleSystem>();
            StartCoroutine(WaitForRest());
        }
    }

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
        timer.Set("StopParticles", sweepTime * 0.5f, () => sweepParticle.Stop());
    }

    private void MoveDown() => transform.position += Vector3.down * fallSpeed * Time.deltaTime;

    private void FinishSweeping()
    {
        isSwept = false;
        isSweeping = false;
        GameEvents.current.WeaponHasBeenSweptAway();
        pooledObject.BackToPool();
    }
}
