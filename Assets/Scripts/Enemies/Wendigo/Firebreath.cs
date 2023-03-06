using System;
using System.Collections.Generic;
using UnityEngine;

public class Firebreath : MonoBehaviour
{
    private const float INFLAMMATION_PROBABILITY = 0.01f;

    [SerializeField] private ParticleSystem firebreathVFX;
    [SerializeField] private GameObject flameVFX;
    [SerializeField] private bool visualizeRaycast;
    [SerializeField] private float deviation = 0.1f;

    private WendigoData data;
    private IObjectPooler pooler;

    private List<Vector3> testHitPoints = new List<Vector3>();

    private void Start()
    {
        pooler = PoolHolder.Instance;
    }

    public void Initialize(WendigoData data) => this.data = data;

    public void Fire()
    {
        firebreathVFX.Play();
    }

    public void Stop()
    {
        firebreathVFX.Stop();
    }

    public void UpdateFire()
    {
        Cast();
    }

    private void Cast()
    {
        testHitPoints.Clear();

        for (int x = -data.FirePrecision; x < data.FirePrecision + 1; x++)
        {
            for (int y = -data.FirePrecision; y < data.FirePrecision + 1; y++)
            {
                Detect(x, y, ComplexLayers.Exploding, PlayFlameVFX);
                Detect(x, y, ComplexLayers.Inflammable, SetOnFire);

                if (visualizeRaycast) 
                    Detect(x, y, ComplexLayers.Exploding, TestRaycast);
            }
        }
    }

    private void Detect(int x, int y, LayerMask mask, Action<RaycastHit> onDetected)
    {
        Vector3 direction = GetDirection(x, y);
        direction = RandomizeDirection(direction);

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, data.FireRange, mask))
        {
            onDetected(hit);
        }
    }

    private Vector3 GetDirection(int x, int y)
    {
        float xOffset = GetOffset(x);
        float yOffset = GetOffset(y);
        Vector3 dir = new Vector3(xOffset, yOffset, 0f) + transform.forward;
        dir = dir.normalized;

        return dir;
    }

    private Vector3 RandomizeDirection(Vector3 direction)
    {
        Vector3 randomVector = ExtraMath.GetRandomVector3() * deviation;
        Vector3 randomizedDirection = (direction + randomVector).normalized;

        return randomizedDirection;
    }

    private float GetOffset(int value) => (value / (float)data.FirePrecision) * data.FireScatter;

    private void PlayFlameVFX(RaycastHit hit)
    {
        if (Rand.Range01() <= INFLAMMATION_PROBABILITY)
            pooler.SpawnFromThePool("SmallFlame", hit.point, Quaternion.identity);
    }

    private void SetOnFire(RaycastHit hit)
    {
        IInflammable inflammable = hit.transform.GetComponent<IInflammable>();
        inflammable.SetOnFire();
    }

    private void TestRaycast(RaycastHit hit) => testHitPoints.Add(hit.point);

    private void OnDrawGizmos()
    {
        if (visualizeRaycast)
        {
            Gizmos.color = Color.red;

            foreach (Vector3 point in testHitPoints)
                Gizmos.DrawLine(transform.position, point);
        }
    }
}
