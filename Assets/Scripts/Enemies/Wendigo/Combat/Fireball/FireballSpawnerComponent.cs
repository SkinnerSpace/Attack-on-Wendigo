using System;
using System.Collections.Generic;
using UnityEngine;
using WendigoCharacter;

public class FireballSpawnerComponent : MonoBehaviour
{
    [SerializeField] private Transform owner;
    [SerializeField] private WendigoData data;
    [SerializeField] private ParticleSystem castVFX;
    [SerializeField] private List<Limb> hands;

    private FireballSpawner fireballSpawner;

    private IObjectPooler pooler;

    private void Awake()
    {
        fireballSpawner = new FireballSpawner(data.FireballSpawner);

        foreach (Limb hand in hands){
            hand.SubscribeOnAmputation(Deactivate);
        }
    }

    private void Start() => pooler = PoolHolder.Instance;

    public void PlayCastVFX() => castVFX.Play();

    public void SpawnFireball()
    {
        Rotate();
        Fireball fireball = pooler.SpawnFromThePool("Fireball", transform.position, transform.rotation).GetComponent<Fireball>();
        fireball.SetOwner(owner);
        fireball.SetTarget(data.Target.transform);
    }

    private void Rotate()
    {
        transform.LookAt(data.Target.Position);
        transform.localEulerAngles = fireballSpawner.GetConstrainedAngles(transform.localEulerAngles);
    }

    private void Deactivate(){
        data.Fireball.IsExist = false;
    }
}
