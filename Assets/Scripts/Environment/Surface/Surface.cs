using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private SurfaceSFXManager sfxManager;

    public SurfaceHitBuilder Hit()
    {
        ParticleSystem particle = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity).GetComponent<ParticleSystem>();
        return new SurfaceHitBuilder(particle, sfxManager);
    }
}
