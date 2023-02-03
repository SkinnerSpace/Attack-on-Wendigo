using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RaycastShooter : MonoBehaviour, IShooter, ICameraUser
{
    [Header("Required Components")]
    [SerializeField] private WeaponData data;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private WeaponVFXController vFXController;
    [SerializeField] private WeaponSFXPlayer sFXPlayer;

    private WeaponSight sight;
    private FunctionTimer timer;

    private bool isReady = true;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
    }

    public void Shoot(bool isFiring, Action onShot)
    {
        if (isFiring && isReady)
        {
            DoShoot();
            onShot?.Invoke();
        }
    }

    private void DoShoot()
    {
        isReady = false;
        HitTheTarget(sight.GetTarget());

        sFXPlayer.PlayShootSFX();
        vFXController.PlayShootVFX();

        timer.Set("CoolDown", data.Rate, CoolDown);
    }

    private void HitTheTarget(WeaponTarget target)
    {
        //Debug.Log("TARGET NAME " + target.name);

        if (target.exist)
        {
            DamageTheTarget(target);
            BlowUpTheSurface(target);
            vFXController.Hit(target);
        }
    }

    private void DamageTheTarget(WeaponTarget target)
    {
        if (target.isDamageable)
        {
            Vector3 impact = CalculateImpact(target.hitDirection, data.Impact);
            DamagePackage damagePackage = new DamagePackage(data.Damage, impact, target.hitPosition);
            target.damageable.ReceiveDamage(damagePackage);
        }
    }

    private void BlowUpTheSurface(WeaponTarget target)
    {
        Surface surface = target.surface;

        if (surface != null)
        {
            surface.Hit().
                    WithPosition(target.hitPosition).
                    WithAngle(target.hitDirection, target.normal).
                    WithShape(0f, 45f).
                    WithCount(20, 30).
                    Launch();
        }
    }
    private void CoolDown() => isReady = true;
    public void ConnectCamera(Camera cam) => sight = new WeaponSight(data, cam);
    private Vector3 CalculateImpact(Vector3 direction, float force) => direction * force;
}
