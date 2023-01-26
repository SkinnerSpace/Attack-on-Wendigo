using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100;

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        health -= damagePackage.damage;
        Debug.Log($"Health {health}");
    }
}
