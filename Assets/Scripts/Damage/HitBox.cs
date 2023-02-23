using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HitBox : MonoBehaviour, IDamageable
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
