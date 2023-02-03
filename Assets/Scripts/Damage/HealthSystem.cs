using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100;

    private event Action onDied;
    private event Action<Vector3, Vector3> triggerRagdoll;

    public void Subscribe(IHealthObserver observer)
    {
        onDied += observer.HasDied;
    }

    public void SubscribeOnRagdoll(IRagdoll ragdoll)
    {
        triggerRagdoll += ragdoll.TriggerRagdoll;
    }

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        health -= damagePackage.damage;

        if (!IsAlive()) triggerRagdoll?.Invoke(damagePackage.impact, damagePackage.point);
    }

    public bool IsAlive() => health > 0;
}
