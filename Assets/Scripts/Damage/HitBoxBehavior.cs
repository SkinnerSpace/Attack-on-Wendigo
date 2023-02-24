using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour, IHitBox
{
    private HitBox hitBox;

    public void Subscribe(IDamageable damageable) => hitBox.Subscribe(damageable);
    public void Unsubscribe(IDamageable damageable) => hitBox.Unsubscribe(damageable);

    public void ReceiveDamage(DamagePackage damagePackage) => hitBox.ReceiveDamage(damagePackage);
}

public class HitBox : IDamageable, IHitBox
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