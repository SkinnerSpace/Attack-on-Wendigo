using System;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private Collider collisionBox;
    private IDamageable damageable;

    private event Action<IDamageable> notifyOnContact;

    private void Awake()
    {
        collisionBox = GetComponent<Collider>();
    }

    public void Subscribe(IDamageBoxObserver observer)
    {
        notifyOnContact += observer.Contacted;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layers.Damageable)
        {
            CauseDamage(collision.transform);

            notifyOnContact?.Invoke(damageable);
            collisionBox.enabled = false;
        }
    }

    private void CauseDamage(Transform victim)
    {
        damageable = victim.GetComponent<IDamageable>();
        DamagePackage damagePackage = new DamagePackage(damage);
        damageable.ReceiveDamage(damagePackage);
    }
}

