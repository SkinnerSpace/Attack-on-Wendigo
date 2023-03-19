using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPhysics : MonoBehaviour
{
    private Rigidbody weaponBody;
    private Collider[] weaponColliders;
    private Levitator levitator;

    public Vector3 Velocity => weaponBody.velocity;
    private bool isLevitating = true;

    private event Action<Collision> onCollisionQuerry;
    private event Action onCollision;

    private void Awake()
    {
        weaponBody = GetComponent<Rigidbody>();
        weaponColliders = GetComponents<Collider>();
        levitator = GetComponent<Levitator>();
    }

    private void Update()
    {
        if (isLevitating)
            levitator.Levitate();
    }

    public void SubscribeOnCollision(Action<Collision> onCollisionQuerry) => this.onCollisionQuerry += onCollisionQuerry;
    public void SubscribeOnCollision(Action onCollision) => this.onCollision += onCollision;

    public void AddForce(Vector3 force) => weaponBody.AddForce(force);
    public void AddTorque(Vector3 torque) => weaponBody.AddTorque(torque);

    public void EnablePhysics()
    {
        ResetVelocity();
        SetColliders(true);
        SetKinematic(false);

        SetLevitation(false);
    }

    public void DisablePhysics()
    {
        ResetVelocity();
        SetColliders(false);
        SetKinematic(true);

        SetLevitation(false);
    }

    private void ResetVelocity() => weaponBody.velocity = Vector3.zero;

    private void SetColliders(bool enabled)
    {
        foreach (Collider weaponCollider in weaponColliders){
            weaponCollider.enabled = enabled;
        }
    }

    private void SetKinematic(bool enabled)
    {
        weaponBody.isKinematic = enabled;
        weaponBody.useGravity = !enabled;
    }

    public void SetLevitation(bool isLevitating) => this.isLevitating = isLevitating;

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionQuerry?.Invoke(collision);
        onCollision?.Invoke();
    }
}
