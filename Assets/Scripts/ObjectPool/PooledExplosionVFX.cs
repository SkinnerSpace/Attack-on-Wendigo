﻿using UnityEngine;

public class PooledExplosionVFX : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => gameObject;
    private ParticleSystem particle;
    private IObjectPooler pooler;

    [SerializeField] private bool permanent;

    private void Awake() => particle = GetComponent<ParticleSystem>();

    private void Start() => pooler = PoolHolder.Instance;

    public void OnObjectSpawn(){
        particle.Play();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    private void OnParticleSystemStopped()
    {
        if (!permanent){
            BackToPool();
        }
    }

    public void BackToPool()
    {
        gameObject.SetActive(false);
        pooler.PutIntoThePool(this);
    }
}
