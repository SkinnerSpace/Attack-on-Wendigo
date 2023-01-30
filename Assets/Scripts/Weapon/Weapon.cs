using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon, ISpeedObserver, IVisionUser
{
    [Header("Required Components")]
    [SerializeField] private Transform shooterImp;
    [SerializeField] private Magazine magazine;
    [SerializeField] private WeaponAimController aimController;
    [SerializeField] private WeaponSwayController swayController;
    [SerializeField] private SkinnedMeshRenderer arms;

    private IShooter shooter;
    private PlayerVision vision;

    public Vector3 DefaultPosition => aimController.DefaultPosition;

    public bool isReady { get; private set; }

    public event Action notifyOnShoot;

    private void Awake()
    {
        shooter = shooterImp.GetComponent<IShooter>();
    }

    public void PullTheTrigger(bool pull)
    {
        shooter.Shoot(pull);
    }

    public void Aim(bool isAiming) => aimController.Aim(isAiming);
    public void Reload() { }
    public void GetReady(bool isReady)
    {
        this.isReady = isReady;
        magazine.GetReady(isReady);
        arms.enabled = isReady;

        if (!isReady) swayController.ResetSway();
    }

    public void ConnectSpeedometer(Speedometer speedometer) => swayController.ConnectSpeedometer(speedometer);
    public void ConnectVision(PlayerVision vision)
    {
        this.vision = vision;

        IVisionUser visionUser = shooterImp.GetComponent<IVisionUser>();
        if (visionUser != null) visionUser.ConnectVision(vision);
    }
}
