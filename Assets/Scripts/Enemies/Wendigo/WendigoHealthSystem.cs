using System;
using UnityEngine;

public class WendigoHealthSystem : IDamageable
{
    private WendigoData data;
    private HitBoxBehavior[] HitBoxes;

    private event Action onDied;
    private event Action<Vector3, Vector3> triggerRagdoll;

    public WendigoHealthSystem(IWendigo wendigo)
    {
        data = wendigo.Data;

        foreach (IHitBox hitBox in wendigo.HitBoxes)
            hitBox.Subscribe(this);
    }

    public void Subscribe(IHealthObserver observer)
    {
        onDied += observer.Die;
    }

    public void SubscribeOnRagdoll(IRagdoll ragdoll)
    {
        triggerRagdoll += ragdoll.TriggerRagdoll;
    }

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        data.Health -= damagePackage.damage;

        if (!IsAlive()) 
            triggerRagdoll?.Invoke(damagePackage.impact, damagePackage.point);
    }

    public bool IsAlive() => data.Health > 0;
}
