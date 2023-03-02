using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionVFX;

    [SerializeField] private FireballData data;
    [SerializeField] private Chronos chronos;
    private FireballMover mover;
    private FireballCollider fireballCollider;
    private FireballExplosion fireballExplosion;

    private bool isActive = true;

    private void Awake()
    {
        mover = new FireballMover(data, chronos);
        fireballCollider = new FireballCollider(data, OnCollision);
        fireballExplosion = new FireballExplosion(data);
    }

    private void Update()
    {
        if (isActive)
            fireballCollider.Update();
    }

    private void FixedUpdate()
    {
        if (isActive)
            mover.Move();
    }

    private void OnCollision()
    {
        isActive = false;
        fireballExplosion.Explode();
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
