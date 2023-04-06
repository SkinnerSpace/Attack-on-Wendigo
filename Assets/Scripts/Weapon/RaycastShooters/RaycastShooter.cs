using System;
using UnityEngine;

public abstract class RaycastShooter
{
    public bool IsReady { get; protected set; } = true;
    protected int shotsCount { get; private set; }

    private event Action onShot;
    private event Action<WeaponTarget> onShotTarget;

    protected WeaponData data;
    protected FunctionTimer timer;
    protected WeaponSight sight;

    public void Shoot()
    {
        if (IsReady)
        {
            IsReady = false;
            shotsCount += 1;
            onShot?.Invoke();
            DoShoot();

            timer.Set("CoolDown", data.Rate, CoolDown);
        }
    }

    private void CoolDown() => IsReady = true;

    protected abstract void DoShoot();

    protected void ShootAtTheTarget()
    {
        WeaponTarget target = sight.GetTarget();
        HitTheTarget(target);
    }

    protected void HitTheTarget(WeaponTarget target)
    {
        if (target.exist)
        {
            onShotTarget?.Invoke(target);
            BlowUpTheSurface(target);
            DamageTheTarget(target);
        }
    }

    protected void BlowUpTheSurface(WeaponTarget target)
    {
        ISurface surface = target.surface;

        if (surface != null){
            DoBlowUpTheSurface(surface, target);
        }
    }

    protected abstract void DoBlowUpTheSurface(ISurface surface, WeaponTarget target);

    protected void DamageTheTarget(WeaponTarget target)
    {
        if (target.isDamageable)
        {
            Vector3 impact = CalculateImpact(target.hitDirection, data.Impact);
            DamagePackage damagePackage = new DamagePackage(data.Damage, impact, target.hitPosition);
            target.damageable.ReceiveDamage(damagePackage);
        }
    }

    private Vector3 CalculateImpact(Vector3 direction, float force) => direction * force;

    public void SubscribeOnShot(Action onShot) => this.onShot += onShot;
    public void UnsubscribeFromOnShot(Action onShot) => this.onShot -= onShot;
    public void SubscribeOnShotTarget(Action<WeaponTarget> onShotTarget) => this.onShotTarget += onShotTarget;
    public void UnsubscribeFromOnShotTarget(Action<WeaponTarget> onShotTarget) => this.onShotTarget -= onShotTarget;

    public void OnReady(bool isReady)
    {
        if (isReady){
            SubscribeOnShot(AimPresenter.Instance.OnShot);
        }
        else{
            UnsubscribeFromOnShot(AimPresenter.Instance.OnShot);
        }
    }

    public void SetCamera(Camera cam) => sight = new WeaponSight(data, cam);
}
