using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon, ICameraUser
{
    [Header("Required Components")]
    [SerializeField] private Transform shooterImp;
    [SerializeField] private Magazine magazine;
    [SerializeField] private WeaponData data;
    [SerializeField] private WeaponAimController aimController;
    [SerializeField] private WeaponSwayController swayController;

    [SerializeField] private List<Transform> cameraUsers;

    private IShooter shooter;
    public Vector3 DefaultPosition => aimController.DefaultPosition;

    public bool isReady { get; private set; }

    private event Action<bool> onReady;

    private void Awake()
    {
        shooter = shooterImp.GetComponent<IShooter>();
    }

    public void Initialize(ICharacterData characterData)
    {
        swayController.Initialize(characterData);
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

    public void Subscribe(IWeaponObserver observer) => onReady += observer.OnReady;

    private void OnShot() => magazine.ReduceCount();

    public void Aim(bool isAiming) => aimController.Aim(isAiming);
    public void Reload() { }
    public void SetReady(bool isReady)
    {
        this.isReady = isReady;
        onReady?.Invoke(isReady);
        magazine.GetReady(isReady);

        if (isReady)
        {
            MainInputReader.Get<CombatInputReader>().Subscribe(this);
        }

        else if (!isReady){
            MainInputReader.Get<CombatInputReader>().Unsubscribe(this);
        }
    }

    public void ConnectCamera(Camera cam)
    {
        foreach (Transform user in cameraUsers)
        {
            ICameraUser cameraUser = user.GetComponent<ICameraUser>();
            cameraUser.ConnectCamera(cam);
        }
    }
}


