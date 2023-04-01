﻿using System;
using UnityEngine;

public class FireballCollider : IFireballCollider
{
    private FireballData data;
    private event Action onCollision;
    private Collider[] hitColliders;

    public FireballCollider(FireballData data, Action onCollision)
    {
        this.data = data;
        this.onCollision += onCollision;

        hitColliders = new Collider[1];
    }

    public void SetHitColliders(Collider[] hitColliders) => this.hitColliders = hitColliders;

    public void Update()
    {
        Physics.OverlapSphereNonAlloc(data.Position, data.CollisionRadius, hitColliders, ComplexLayers.ProjectileCollidable);
        NotifyOnCollision();
    }

    public void NotifyOnCollision()
    {
        if (hitColliders[0] != null)
            onCollision?.Invoke();
    }
}