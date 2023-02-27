using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float impact;

    [SerializeField] private int ammoCapacity;
    [SerializeField] private int ammo;
    [Range(0f, 1f)]
    [SerializeField] private float precision;
    [Range(0f, 1f)]
    [SerializeField] private float aimPrecision;

    [SerializeField] private float rate;

    public int Damage => damage;
    public float Impact => impact;
    public int AmmoCapacity => ammoCapacity;
    public int Ammo => ammo;
    public float Precision => Mathf.Lerp(precision, 1f, precisionAdjustment);
    public float AimPrecision => aimPrecision;
    public float Rate => rate;
    private float precisionAdjustment = 0f;
    public bool IsReady { get; set; }

    public void SetAmmo(int ammo) => this.ammo = ammo;
    public void SetPrecisionAdjustrment(float precisionAdjustment) => this.precisionAdjustment = precisionAdjustment;
}
