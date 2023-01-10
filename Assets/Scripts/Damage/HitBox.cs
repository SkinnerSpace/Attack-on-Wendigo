using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HitBox : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform enemy;
    private IDamageable damageable;

    private void Awake()
    {
        damageable = enemy.GetComponent<IDamageable>();
    }

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        damageable.ReceiveDamage(damagePackage);
    }
}
