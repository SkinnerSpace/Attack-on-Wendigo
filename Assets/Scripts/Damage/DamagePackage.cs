using UnityEngine;

public class DamagePackage
{
    public float damage;
    public Vector3 impact;
    public Vector3 point;

    public DamagePackage(float damage)
    {
        this.damage = damage;
    }

    public DamagePackage(float damage, Vector3 impact, Vector3 point)
    {
        this.damage = damage;
        this.impact = impact;
        this.point = point;
    }
}