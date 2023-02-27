﻿using System;
using UnityEngine;

public class WendigoHealthSystem : WendigoBaseController, IDamageable
{
    private WendigoData data;

    private event Action onDeath;
    private event Action<Vector3, Vector3> onTriggerRagdoll;

    public override void Initialize(IWendigo wendigo)
    {
        data = wendigo.Data;

        foreach (IHitBox hitBox in wendigo.HitBoxes)
            hitBox.Subscribe(this);
    }

    public void Subscribe(Action onDeath) => this.onDeath += onDeath;

    public void SubscribeOnRagdoll(Action<Vector3, Vector3> onTriggerRagdoll) => this.onTriggerRagdoll += onTriggerRagdoll;

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        data.Health -= damagePackage.damage;

        if (!IsAlive())
        {
            onTriggerRagdoll?.Invoke(damagePackage.impact, damagePackage.point);
            onDeath?.Invoke();
        }
    }

    public bool IsAlive() => data.Health > 0;
}
