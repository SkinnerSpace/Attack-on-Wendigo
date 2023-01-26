using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile, IDamageBoxObserver
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private DamageBox damageBox;

    private void Awake()
    {
        damageBox.Subscribe(this);
    }

    public void Launch(Vector3 force)
    {
        body.AddForce(force, ForceMode.Impulse);
    }

    public void Contacted(IDamageable damageable)
    {
        Destroy(gameObject);
    }
}

public interface IDamageBoxObserver
{
    void Contacted(IDamageable damageable);
}

