using UnityEngine;

public interface IProjectile
{
    void Launch(Vector3 force);
    void Contacted(IDamageable damageable);
}
