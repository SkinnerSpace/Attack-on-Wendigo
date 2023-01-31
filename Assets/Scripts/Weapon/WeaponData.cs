using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private int ammo;
    [Range(0f, 1f)]
    [SerializeField] private float precision;
    [SerializeField] private float rate;

    public float Damage => damage;
    public int Ammo => ammo;
    public float Precision => precision;
    public float Rate => rate;

    public void SetAmmo(int ammo) => this.ammo = ammo; 
}