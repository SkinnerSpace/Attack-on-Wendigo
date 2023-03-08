using System;
using System.Collections.Generic;
using UnityEngine;

public class Firebreath : MonoBehaviour
{
    private const float INFLAMMATION_PROBABILITY = 0.5f;

    [Header("Required Components")]
    [SerializeField] private ParticleSystem firebreathVFX;
    [SerializeField] private GameObject flameVFX;
    [SerializeField] private Chronos chronos;

    [Header("Settings")]
    [SerializeField] private bool visualizeRaycast;
    [SerializeField] private float deviation = 0.1f;
    [SerializeField] private float flameVFXTime = 0.3f;

    private WendigoData data;
    private IObjectPooler pooler;

    private List<Vector3> testHitPoints = new List<Vector3>();
    private float currentFlameVFXTime;

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
/*        currentFlameVFXTime += chronos.DeltaTime;*/

        CastRayFountain();

/*        if (FlameVFXTimeOut())
        {
            currentFlameVFXTime = 0f;
        }*/
    }

    private void CastRayFountain()
    {
        for (int x = -data.FirePrecision; x < data.FirePrecision + 1; x++)
        {
            for (int y = -data.FirePrecision; y < data.FirePrecision + 1; y++)
            {
                ExecuteDetection(x, y);
            }
        }
    }

    private void ExecuteDetection(int x, int y)
    {
        IntVector2 vector = new IntVector2(x, y);

        Detect(vector, ComplexLayers.Inflammable, SetOnFire);
        //PlayFlameVFXOnTime(vector);

        if (visualizeRaycast)
            Detect(vector, ComplexLayers.Exploding, TestRaycast);
    }

    private void Detect(IntVector2 vector, LayerMask mask, Action<RaycastHit> onDetected)
    {
        Vector3 direction = GetDirection(vector);
        direction = RandomizeDirection(direction);

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, data.FireRange, mask))
            onDetected(hit);
    }

    private Vector3 GetDirection(IntVector2 vector)
    {
        float xOffset = GetOffset(vector.x);
        float yOffset = GetOffset(vector.y);
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

    private void PlayFlameVFXOnTime(IntVector2 vector)
    {
        if (FlameVFXTimeOut())
            Detect(vector, ComplexLayers.Exploding, PlayFlameVFX);
    }

    private bool FlameVFXTimeOut() => currentFlameVFXTime >= flameVFXTime;

    private void PlayFlameVFX(RaycastHit hit)
    {
        pooler.SpawnFromThePool("SmallFlame", hit.point, Quaternion.identity);
        Debug.Log("PLAY!");
      /*  if (Rand.Range01() >= INFLAMMATION_PROBABILITY)
            pooler.SpawnFromThePool("SmallFlame", hit.point, Quaternion.identity);*/
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
