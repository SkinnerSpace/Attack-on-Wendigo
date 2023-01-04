using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField] private GunSight gunSight;
    [SerializeField] private GunMagazine gunMagazine;
    [SerializeField] private GunBarrel gunBarrel;

    [Header("Controllers")]
    [SerializeField] private WeaponAimController aimController;
    [SerializeField] private WeaponRecoilController recoilController;

    public Action notifyOutOfAmmo;

    private void Start()
    {
        notifyOutOfAmmo += AmmoBar.Instance.UpdateOutOfAmmo;
    }

    public void PullTheTrigger()
    {
        if (gunBarrel.isReady)
            ShootIfEnoughAmmo();
    }

    private void ShootIfEnoughAmmo()
    {
        if (!gunMagazine.IsEmpty())
        {
            Shoot();
        }
        else
        {
            notifyOutOfAmmo();
        }
    }

    private void Shoot()
    {
        gunBarrel.bullet = gunMagazine.TakeAmmo();
        gunBarrel.Shoot();
        recoilController.Recoil();

        //sFXPlayer.play(sFXLibrary.shootSFX);

        DamageTargetIfExist();
    }

    private void DamageTargetIfExist()
    {
        if (gunSight.Damageable != null)
        {
            DamagePackage damagePackage = new DamagePackage(1f);
            gunSight.Damageable.ReceiveDamage(damagePackage);
        }
    }

    public void Aim(bool isAiming)
    {
        aimController.Aim(isAiming);
    }

    public void Reload()
    {
        gunMagazine.Reload();
    }
}
