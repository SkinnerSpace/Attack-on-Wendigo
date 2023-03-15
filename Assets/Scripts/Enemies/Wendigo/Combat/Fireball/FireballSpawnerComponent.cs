using System;
using UnityEngine;
using WendigoCharacter;

public class FireballSpawnerComponent : MonoBehaviour
{
    [SerializeField] private WendigoData data;
    [SerializeField] private ParticleSystem castVFX;

    private FireballSpawner fireballSpawner;

    private IObjectPooler pooler;

    private void Awake()
    {
        fireballSpawner = new FireballSpawner(data.FireballSpawner);
    }

    private void Start() => pooler = PoolHolder.Instance;

    public void PlayCastVFX() => castVFX.Play();

    public void SpawnFireball()
    {
        Rotate();
        pooler.SpawnFromThePool("Fireball", transform.position, transform.rotation);
    }

    private void Rotate()
    {
        transform.LookAt(data.Target.Position);
        transform.localEulerAngles = fireballSpawner.GetConstrainedAngles(transform.localEulerAngles);
    }
}
