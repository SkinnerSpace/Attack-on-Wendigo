using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RaycastShooter 
{
    private WeaponData data;
    private FunctionTimer timer;
    private WeaponSight sight;

    public bool IsReady { get; set; } = true;

    private event Action onShot;
    private event Action<WeaponTarget> onShotTarget;

    public RaycastShooter(WeaponData data, FunctionTimer timer)
    {
        this.data = data;
        this.timer = timer;
    }

    public void SetCamera(Camera cam) => sight = new WeaponSight(data, cam);

    public void SubscribeOnShot(Action onShot) => this.onShot += onShot;
    public void SubscribeOnShotTarget(Action<WeaponTarget> onShotTarget) => this.onShotTarget += onShotTarget;

    public void Shoot()
    {
        if (IsReady)
        {
            IsReady = false;
            WeaponTarget target = sight.GetTarget();
            HitTheTarget(target);
            onShot?.Invoke();

            timer.Set("CoolDown", data.Rate, CoolDown);
        }
    }

    private void HitTheTarget(WeaponTarget target)
    {
        if (target.exist)
        {
            DamageTheTarget(target);
            BlowUpTheSurface(target);
            onShotTarget?.Invoke(target);
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
        ISurface surface = target.surface;

        if (surface != null)
        {
            surface.Hit().
                    WithPosition(target.hitPosition).
                    WithAngle(target.hitDirection, target.normal).
                  /*  WithShape(0f, 45f).
                    WithCount(20, 30).*/
                    Launch();
        }
    }
    private void CoolDown() => IsReady = true;
    public void ConnectCamera(Camera cam) => sight = new WeaponSight(data, cam);
    private Vector3 CalculateImpact(Vector3 direction, float force) => direction * force;
}
