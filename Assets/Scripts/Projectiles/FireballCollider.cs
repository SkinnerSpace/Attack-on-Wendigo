using System;
using UnityEngine;

public class FireballCollider : IFireballCollider
{
    private FireballData data;
    private event Action onCollision;

    public FireballCollider(FireballData data, Action onCollision)
    {
        this.data = data;
        this.onCollision += onCollision;
    }

    public void Update()
    {
        Collider[] hitColliders = new Collider[1];
        Physics.OverlapSphereNonAlloc(data.Position, data.CollisionRadius, hitColliders, ComplexLayers.Exploding);

        if (hitColliders[0] != null)
            onCollision?.Invoke();
    }
}
