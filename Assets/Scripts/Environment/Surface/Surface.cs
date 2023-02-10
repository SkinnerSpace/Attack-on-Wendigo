using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private string particleName;
    [SerializeField] private SurfaceSFXManager sfxManager;

    private IObjectPooler pooler;

    private void Start() => pooler = PoolHolder.Instance;

    public SurfaceHitBuilder Hit()
    {
        ParticleSystem particle = pooler.SpawnFromThePool(particleName, Vector3.zero, Quaternion.identity).transform.GetComponent<ParticleSystem>();
        return new SurfaceHitBuilder(particle, sfxManager);
    }
}
