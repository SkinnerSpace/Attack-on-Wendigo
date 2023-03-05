using UnityEngine;

public class RadialSurfaceHitHandler
{
    private FireballData data;

    public RadialSurfaceHitHandler(FireballData data) => this.data = data;

    public void RadiallyHitTheSurface()
    {
        for (int x = -data.Detail; x < data.Detail + 1; x++)
        {
            for (int y = -data.Detail; y < data.Detail + 1; y++)
            {
                for (int z = -data.Detail; z < data.Detail + 1; z++)
                {
                    Cast(x, y, z);
                }
            }
        }
    }

    private void Cast(int x, int y, int z)
    {
        Vector3 direction = GetDirection(x, y, z);

        if (direction != Vector3.zero)
        {
            direction = direction.normalized;
            if (Physics.Raycast(data.Position, direction, out RaycastHit hit, data.ExplosionRadius * 10f, ComplexLayers.Exploding))
                BlowUpTheSurface(hit);
        }
    }

    private Vector3 GetDirection(int x, int y, int z)
    {
        Vector3 direction = new Vector3(
        direction.x = x != 0 ? x / (float)data.Detail : 0,
        direction.y = y != 0 ? y / (float)data.Detail : 0,
        direction.z = z != 0 ? z / (float)data.Detail : 0
        );

        return direction;
    }

    private void BlowUpTheSurface(RaycastHit hit)
    {
        Surface surface = hit.transform.GetComponent<Surface>();

        if (surface != null)
        {
            Vector3 direction = (hit.point - data.Position).normalized;

            surface.Hit().
                    WithPosition(hit.point).
                    WithScale(3).
                    WithAngle(direction, hit.normal).
                    WithShape(2f, 40f).
                    WithCount(5, 10).
                    Launch();
        }
    }
}
