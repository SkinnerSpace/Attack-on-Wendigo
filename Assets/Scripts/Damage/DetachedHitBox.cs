using System;
using UnityEngine;

public class DetachedHitBox : IDamageable, IHitBox
{
    private event Action<DamagePackage> onDamage;

    public void Subscribe(IDamageable damageable)
    {
        Debug.Log("Subscribe");
        onDamage += damageable.ReceiveDamage;
    }
    public void Unsubscribe(IDamageable damageable) => onDamage -= damageable.ReceiveDamage;

    public void ReceiveDamage(DamagePackage damagePackage) => onDamage(damagePackage);
}