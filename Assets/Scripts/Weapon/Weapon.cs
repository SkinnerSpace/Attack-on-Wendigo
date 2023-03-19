using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [Header("Required Components")]
    [SerializeField] private WeaponData data;
    [SerializeField] private WeaponAimController aimController; 
    [SerializeField] private WeaponSwayController swayController;
    [SerializeField] private Pickable pickable;
    [SerializeField] private WeaponPhysics physics;
    [SerializeField] private FunctionTimer timer;

    [Header("Effects")]
    [SerializeField] private WeaponVFXController vFXController;
    [SerializeField] private WeaponSFXPlayer sFXPlayer;
    [SerializeField] private WeaponAnimationController animationController;
    
    private IInputReader inputReader;
    private RaycastShooter shooter;
    private Magazine magazine;
    private WeaponHitSurfaceHandler surfaceHitHandler;
    public Vector3 DefaultPosition => aimController.DefaultPosition;

    private event Action<bool> onReadinessUpdate;
    private event Action onReady;

    private void Awake()
    {
        CreateShooter();
        CreateMagazine();
        CreateSurfaceHitHandler();
    }

    private void CreateSurfaceHitHandler()
    {
        surfaceHitHandler = new WeaponHitSurfaceHandler();
        physics.SubscribeOnCollision(surfaceHitHandler.HitTheSurface);
        physics.SubscribeOnCollision(DisposeUsedWeapon);
    }

    private void CreateShooter()
    {
        shooter = new RaycastShooter(data, timer);
        shooter.SubscribeOnShot(vFXController.PlayShootVFX);
        shooter.SubscribeOnShotTarget(vFXController.Hit);
        shooter.SubscribeOnShot(sFXPlayer.PlayShootSFX);
        shooter.SubscribeOnShot(animationController.PlayShoot);
    }

    private void CreateMagazine()
    {
        magazine = new Magazine(data, timer);
        magazine.SubscribeOnEmpty(sFXPlayer.PlayIsEmptySFX);
        SubscribeOnReadinessUpdate(magazine.OnReady);
    }

    public void InitializeOnTake(ICharacterData characterData, IInputReader inputReader)
    {
        this.inputReader = inputReader;
        swayController.Initialize(characterData, inputReader);
        shooter.SetCamera(characterData.Cam);
    }

    public void PressTheTrigger()
    {
        if (!magazine.HasAmmo())
            magazine.NotifyOnEmpty();
    }

    public void HoldTheTrigger()
    {
        if (magazine.HasAmmo() && shooter.IsReady)
        {
            magazine.ReduceCount();
            shooter.Shoot();
        }
    }

    public void SubscribeOnReadinessUpdate(Action<bool> onReadinessUpdate) => this.onReadinessUpdate += onReadinessUpdate;
    public void SubscribeOnReady(Action onReady) => this.onReady += onReady;

    public void Aim(bool isAiming) => aimController.Aim(isAiming);
    public void Reload() => magazine.RestoreAmmo();
    public void SetReady(bool isReady)
    {
        data.IsReady = isReady;
        onReadinessUpdate?.Invoke(isReady);
        ManageConnectionToInput(isReady);
        NotifyOnReady(isReady);
    }

    private void ManageConnectionToInput(bool connect)
    {
        if (connect){
            inputReader.Get<CombatInputReader>().Subscribe(this);
        }
        else{
            inputReader.Get<CombatInputReader>().Unsubscribe(this);
        }
    }

    private void NotifyOnReady(bool isReady)
    {
        if (isReady){
            onReady?.Invoke();
        }
    }

    private void DisposeUsedWeapon()
    {
        if (magazine.IsEmpty() && pickable.IsReadyToHand)
            pickable.SwitchOff();
    }
}

