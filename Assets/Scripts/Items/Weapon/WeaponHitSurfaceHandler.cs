using UnityEngine;

public class WeaponHitSurfaceHandler
{
    private const float HIT_INTERVAL = 0.1f;

    private bool isAbleToHit;
    private FunctionTimer timer;

    public WeaponHitSurfaceHandler(FunctionTimer timer)
    {
        this.timer = timer;
    }

    public void HitTheSurface(Collision collision)
    {
        if (isAbleToHit)
        {
            ISurface surface = collision.transform.GetComponent<ISurface>();

            if (surface != null)
            {
                isAbleToHit = false;
                timer.Set("Unlock", HIT_INTERVAL, () => isAbleToHit = true);

                surface.Hit().
                        WithPosition(collision.GetHitPoint()).
                        WithAngle(Vector3.down, Vector3.up).
                        WithShape(0.5f, 70f).
                        WithCount(2, 4).
                        Launch();
            }
        }
    }
}

