using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100;

    private event Action onDied;

    public void Subscribe(IHealthObserver observer)
    {
        onDied += observer.HasDied;
    }

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        health -= damagePackage.damage;
        Debug.Log($"Health {health}");

        if (!IsAlive()) onDied();
    }

    public bool IsAlive() => health > 0;
}

public interface IHealthObserver
{
    void HasDied();
}
