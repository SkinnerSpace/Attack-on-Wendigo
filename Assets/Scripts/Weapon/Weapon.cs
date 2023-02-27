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
    
    private IInputReader inputReader;
    private RaycastShooter shooter;
    private Magazine magazine;
    private WeaponHitSurfaceHandler surfaceHitHandler;
    public Vector3 DefaultPosition => aimController.DefaultPosition;
    public WeaponData Data => data;
    public FunctionTimer Timer => timer;

    private event Action<bool> onReady;

    private void Awake()
    {
        surfaceHitHandler = new WeaponHitSurfaceHandler();

        physics.SubscribeOnCollision(surfaceHitHandler.HitTheSurface);
        physics.SubscribeOnCollision(DisposeUsedWeapon);

        shooter = new RaycastShooter(data, timer);
        magazine = new Magazine(data, timer);

        shooter.SubscribeOnShot(vFXController.PlayShootVFX);
        shooter.SubscribeOnShot(sFXPlayer.PlayShootSFX);
        shooter.SubscribeOnShotTarget(vFXController.Hit);

        magazine.SubscribeOnEmpty(sFXPlayer.PlayIsEmptySFX);

        SubscribeOnReady(magazine.OnReady);
    }

    public void Initialize(ICharacterData characterData, IInputReader inputReader)
    {
        this.inputReader = inputReader;
        swayController.Initialize(this, characterData, inputReader);
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

    public void SubscribeOnReady(Action<bool> onReady) => this.onReady += onReady;

    public void Aim(bool isAiming) => aimController.Aim(isAiming);
    public void Reload() => magazine.RestoreAmmo();
    public void SetReady(bool isReady)
    {
        data.IsReady = isReady;
        onReady?.Invoke(isReady);

        if (isReady)
            inputReader.Get<CombatInputReader>().Subscribe(this);

        else if (!isReady)
            inputReader.Get<CombatInputReader>().Unsubscribe(this);
    }

    private void DisposeUsedWeapon()
    {
        if (magazine.IsEmpty() && pickable.IsReadyToHand)
            pickable.SwitchOff();
    }
}

