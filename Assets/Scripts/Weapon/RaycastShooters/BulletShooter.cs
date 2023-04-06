using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BulletShooter : RaycastShooter
{
    public BulletShooter(WeaponData data, FunctionTimer timer)
    {
        this.data = data;
        this.timer = timer;
    }

    protected override void DoShoot()
    {
        ShootAtTheTarget();
    }

    protected override void DoBlowUpTheSurface(ISurface surface, WeaponTarget target)
    {
        surface.Hit("Bullet").
                    WithPosition(target.hitPosition).
                    WithAngle(target.hitDirection, target.normal).
                    Launch();
    }
}
