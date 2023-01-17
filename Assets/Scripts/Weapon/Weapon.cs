using System;
using System.Collections.Generic;
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

    private IHoldable[] holdables;
    private IReleaseable[] releaseables;

    private void Awake()
    {
        holdables = GetComponentsInChildren<IHoldable>();
        releaseables = GetComponentsInChildren<IReleaseable>();

        Debug.Log("Holdables " + holdables.Length);
        Debug.Log("Releaseables " + releaseables.Length);
    }

    private void Start()
    {
        notifyOutOfAmmo += AmmoBar.Instance.UpdateOutOfAmmo;
    }

    public void PullTheTrigger()
    {
        foreach (IHoldable holdable in holdables)
            holdable.Hold();

        if (gunBarrel.isReady)
            ShootIfEnoughAmmo();
    }

    public void ReleaseTheTrigger()
    {
        foreach (IReleaseable releaseable in releaseables)
            releaseable.Release();

        //Debug.Log("release");
    }

    private void ShootIfEnoughAmmo()
    {
        if (!gunMagazine.IsEmpty()) Shoot();
        else notifyOutOfAmmo();
    }

    private void Shoot()
    {
        gunBarrel.Load(gunMagazine.TakeAmmo());
        gunBarrel.Shoot();
        recoilController.Recoil();

        //DamageTargetIfExist();
    }

    private void DamageTargetIfExist()
    {
        if (gunSight.targetExist)
        {
            DamagePackage damagePackage = new DamagePackage(1f);
            gunSight.Damageable.ReceiveDamage(damagePackage);
        }
    }

    public void Aim(bool isAiming) => aimController.Aim(isAiming);
    public void Reload() => gunMagazine.Reload();
}

public interface IHoldable
{
    void Hold();
}

public interface IReleaseable
{
    void Release();
}
