using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, ISwitchable
{
    [SerializeField] private FireballData data;
    [SerializeField] private Chronos chronos;
    [SerializeField] private FireballSFXPlayer sFXPlayer;

    private FireballMover mover;
    private FireballCollider fireballCollider;
    private FireballExplosion fireballExplosion;
    private RadialSurfaceHitHandler hitHandler;

    private event Action onExplosion;

    private void Awake()
    {
        mover = new FireballMover(data, chronos);
        fireballCollider = new FireballCollider(data, Explode);
        fireballExplosion = new FireballExplosion(data);
        hitHandler = new RadialSurfaceHitHandler(data);
    }

    private void Update()
    {
        if (data.IsActive)
        {
            fireballCollider.Update();
            ExplodeWhenBeyondTheBoundaries();
        }
    }

    private void FixedUpdate()
    {
        if (data.IsActive)
            mover.Move();
    }

    private void ExplodeWhenBeyondTheBoundaries()
    {
        if (IsBeyondTheBoundaries())
            Explode();
    }

    private bool IsBeyondTheBoundaries()
    {
        return (transform.position.x <= 0f ||
                transform.position.x >= data.MaxHorizontalBoundary ||
                transform.position.z <= 0f ||
                transform.position.z >= data.MaxHorizontalBoundary ||
                transform.position.y >= data.MaxVerticalBoundary
                );
    }

    public void Subscribe(Action onExplosion) => this.onExplosion += onExplosion;

    private void Explode()
    {
        data.IsActive = false;
        sFXPlayer.PlayExplosionSFX();
        fireballExplosion.Explode();
        onExplosion?.Invoke();

        hitHandler.RadiallyHitTheSurface();
    }

    public void SetOwner(Transform owner) => data.owner = owner;

    public void SetTarget(Transform target) => data.target = target;

    public void SwitchOn()
    {
        data.IsActive = true;
        data.TimeToFullHomingLeft = data.InitialTimeToFullHoming;
    }

    public void SwitchOff() => data.IsActive = false;
}
