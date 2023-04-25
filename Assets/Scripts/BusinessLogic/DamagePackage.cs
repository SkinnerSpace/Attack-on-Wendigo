using UnityEngine;

public partial class DamagePackage
{
    public int damage;
    public Vector3 impact;
    public Vector3 point;
    public DamageTypes damageType; 

    private DamagePackage() { }

    public DamagePackage(int damage, Vector3 impact, Vector3 point, DamageTypes damageType) : this(damage, impact, point){
        this.damageType = damageType;
    }

    public DamagePackage(int damage, Vector3 impact, Vector3 point) : this (damage){
        this.impact = impact;
        this.point = point;
    }

    public DamagePackage(int damage){
        this.damage = damage;
    }

    public override string ToString() => "Type" + damageType + ", Damage: " + damage + ", Impact: " + impact + ", Point: " + point;
}