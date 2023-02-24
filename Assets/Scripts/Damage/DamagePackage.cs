using UnityEngine;

public class DamagePackage
{
    public int damage;
    public Vector3 impact;
    public Vector3 point;

    public DamagePackage(int damage)
    {
        this.damage = damage;
    }

    public DamagePackage(int damage, Vector3 impact, Vector3 point)
    {
        this.damage = damage;
        this.impact = impact;
        this.point = point;
    }
}