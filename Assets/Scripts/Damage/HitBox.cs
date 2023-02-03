using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HitBox : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthSystem healthSystem;

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        healthSystem.ReceiveDamage(damagePackage);
    }
}
