using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public void ReceiveDamage(DamagePackage damagePackage)
    {
        Destroy(gameObject);
    }
}
