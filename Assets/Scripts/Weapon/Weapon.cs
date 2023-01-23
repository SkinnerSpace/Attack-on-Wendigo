using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon, ISpeedObserver
{
    [Header("Parts")]
    [SerializeField] private GunSight gunSight;
    [SerializeField] private GunMagazine gunMagazine;
    [SerializeField] private GunBarrel gunBarrel;

    [Header("Controllers")]
    [SerializeField] private WeaponAimController aimController;
    [SerializeField] private WeaponRecoilController recoilController;
    [SerializeField] private WeaponSwayController swayController;

    public Vector3 DefaultPosition => aimController.DefaultPosition;

    public Action notifyOutOfAmmo;

    public bool isReady { get; private set; }

    private void Start()
    {
        notifyOutOfAmmo += AmmoBar.Instance.UpdateOutOfAmmo;
    }

    public void PullTheTrigger(bool pull)
    {
        if (pull) Debug.Log("PULL");
    }

    public void Aim(bool isAiming) => aimController.Aim(isAiming);
    public void Reload() => gunMagazine.Reload();
    public void GetReady(bool isReady) => this.isReady = isReady;

    public void ConnectSpeedometer(Speedometer speedometer) => swayController.ConnectSpeedometer(speedometer);
}

