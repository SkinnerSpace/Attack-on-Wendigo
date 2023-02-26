using UnityEngine;

public class WeaponHitSurfaceHandler
{
    public void HitTheSurface(Collision collision)
    {
        Surface surface = collision.transform.GetComponent<Surface>();

        if (surface != null)
        {
            surface.Hit().
                    WithPosition(collision.GetHitPoint()).
                    WithAngle(Vector3.down, Vector3.up).
                    WithShape(0.5f, 70f).
                    WithCount(2, 4).
                    Launch();
        }
    }
}

