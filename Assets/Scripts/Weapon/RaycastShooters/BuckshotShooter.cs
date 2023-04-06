using UnityEngine;

public class BuckshotShooter : RaycastShooter
{
    private const int BUCKSHOT_COUNT = 10;

    public BuckshotShooter(WeaponData data, FunctionTimer timer)
    {
        this.data = data;
        this.timer = timer;
    }

    protected override void DoShoot()
    {
        for (int i = 0; i < BUCKSHOT_COUNT; i++){
            ShootAtTheTarget();
        }
    }

    protected override void DoBlowUpTheSurface(ISurface surface, WeaponTarget target)
    {
        surface.Hit("Buckshot").
                    WithPosition(target.hitPosition).
                    WithAngle(target.hitDirection, target.normal).
                    WithSFXID(this, shotsCount).
                    Launch();
    }
}