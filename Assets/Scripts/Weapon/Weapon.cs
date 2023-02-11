using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon, ISpeedObserver, ICameraUser
{
    [Header("Required Components")]
    [SerializeField] private Transform shooterImp;
    [SerializeField] private Magazine magazine;

    [SerializeField] private WeaponAimController aimController;
    [SerializeField] private WeaponSwayController swayController;
    [SerializeField] private SkinnedMeshRenderer arms;

    [SerializeField] private List<Transform> cameraUsers;

    private IShooter shooter;
    private Collider collisionBox;
    public Vector3 DefaultPosition => aimController.DefaultPosition;

    public bool isReady { get; private set; }

    public event Action<bool> onReady;

    private void Awake()
    {
        shooter = shooterImp.GetComponent<IShooter>();
        collisionBox = GetComponent<Collider>();
    }

    public void PullTheTrigger()
    {
        if (magazine.HasAmmo()){
            shooter.Shoot(true, OnShot);
        }
        else{
            magazine.ReportIsEmpty();
        }
    }

    private void OnShot() => magazine.ReduceCount();

    public void Aim(bool isAiming) => aimController.Aim(isAiming);
    public void Reload() { }
    public void SetReady(bool isReady)
    {
        this.isReady = isReady;
        onReady?.Invoke(isReady);
        magazine.GetReady(isReady);
        arms.enabled = isReady;

        if (isReady){
            collisionBox.enabled = false;
        }
        else if (!isReady){
            swayController.ResetSway();
            collisionBox.enabled = true;
        }
    }

    public void ConnectSpeedometer(Speedometer speedometer) => swayController.ConnectSpeedometer(speedometer);

    public void ConnectCamera(Camera cam)
    {
        foreach (Transform user in cameraUsers)
        {
            ICameraUser cameraUser = user.GetComponent<ICameraUser>();
            cameraUser.ConnectCamera(cam);
        }
    }
}

