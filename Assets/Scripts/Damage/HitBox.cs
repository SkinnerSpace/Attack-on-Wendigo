using System;

public class HitBox : IHitBox
{
    private event Action<DamagePackage> onDamage;

    public void Subscribe(IDamageable damageable) => onDamage += damageable.ReceiveDamage;
    public void Unsubscribe(IDamageable damageable) => onDamage -= damageable.ReceiveDamage;
    public void ReceiveDamage(DamagePackage damagePackage) => onDamage?.Invoke(damagePackage);
}
