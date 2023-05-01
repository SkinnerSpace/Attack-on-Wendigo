using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon, IHandyItem
{
    private static int id;

    [SerializeField] private string weaponName;

    [Header("Required Components")]
    [SerializeField] private WeaponData data;
    [SerializeField] private WeaponAimController aimController; 
    [SerializeField] private SwayController swayController;
    [SerializeField] private Pickable pickable;
    [SerializeField] private ItemSweeper sweeper;
    [SerializeField] private ItemPhysicalBody physics;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private AbandonmentDetector abandonmentDetector;
    [SerializeField] private AimAnimationsPack animationsPack;

    [Header("Effects")]
    [SerializeField] private WeaponVFXController vFXController;
    [SerializeField] private WeaponSFXPlayer sFXPlayer;
    [SerializeField] private WeaponAnimationController animationController;
    
    private IInputReader inputReader;
    private IInteractionController interactor;
    private RaycastShooter shooter;
    private Magazine magazine;
    private WeaponHitSurfaceHandler surfaceHitHandler;
    public Vector3 DefaultPosition => aimController.DefaultPosition;

    public AimAnimationsPack AimAnimation => animationsPack;
    public float Rate => data.Rate;

    private event Action onReady;
    private event Action onNotReady;

    private void Awake()
    {
        AssignName();

        CreateShooter();
        CreateMagazine();
        CreateSurfaceHitHandler();

        swayController.InitializeOnAwake(this, aimController);
    }

    private void AssignName() => transform.name = weaponName + "_" + (++id);

    private void CreateSurfaceHitHandler()
    {
        surfaceHitHandler = new WeaponHitSurfaceHandler();
        physics.onCollisionQuerry += surfaceHitHandler.HitTheSurface;
        physics.onCollision += DisposeUsedWeapon;
    }

    private void CreateShooter()
    {
        shooter = RaycastShooterFactory.Create(data.ShooterType, data, timer);
        shooter.SubscribeOnShot(vFXController.PlayShootVFX);
        shooter.SubscribeOnShotTarget(vFXController.Hit);
        shooter.SubscribeOnShot(sFXPlayer.PlayShootSFX);
        shooter.SubscribeOnShot(animationController.PlayAnimationIfPossible);
    }

    private void CreateMagazine()
    {
        magazine = new Magazine(data, timer);
        magazine.onOutOfAmmo += sFXPlayer.PlayIsEmptySFX;

        SubscribeOnReady(shooter.OnReady);
        SubscribeOnNotReady(shooter.OnNotReady);
    }

    public void InitializeOnTake(ICharacterData characterData, IInputReader inputReader, IInteractionController interactor)
    {
        this.inputReader = inputReader;
        this.interactor = interactor;

        swayController.InitializeOnTake(characterData, inputReader);
        abandonmentDetector.Initialize(characterData);
        shooter.SetCamera(characterData.Cam);

        magazine.NotifyOnAmmoUpdate();
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

    public void SubscribeOnReady(Action onReady) => this.onReady += onReady;
    public void SubscribeOnNotReady(Action onNotReady) => this.onNotReady += onNotReady;

    public void Aim(bool isAiming) => aimController.Aim(isAiming);
    public void Reload() => magazine.RestoreAmmo();
    public void SetReady(bool isReady)
    {
        data.IsReady = isReady;

        if (isReady)
        {
            onReady?.Invoke();
            inputReader.Get<CombatInputReader>().Subscribe(this);
        }
        else
        {
            onNotReady?.Invoke();
            PlayerEvents.current.ThrowWeapon();
            inputReader.Get<CombatInputReader>().Unsubscribe(this);

        }
    }

    private void DisposeUsedWeapon()
    {
        if (WeaponIsDisposed()){
            pickable.SwitchOff();
            sweeper.SweepTheWeapon();
        }
    }

    private bool WeaponIsDisposed()
    {
        return magazine.IsEmpty() &&
               pickable.IsReadyToHand;
    }
}
