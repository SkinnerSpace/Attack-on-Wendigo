using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPhysics : MonoBehaviour
{
    [SerializeField] private float groundCheckRadius = 1f;

    private Rigidbody weaponBody;
    private Collider[] weaponColliders;
    private Levitator levitator;
    private WeaponGroundDetector groundDetector;

    public Vector3 Velocity => weaponBody.velocity;

    private bool physicsIsEnabled;
    private bool isLevitating = true;

    private event Action<Collision> onCollisionQuerry;
    private event Action onCollision;
    
    private void Awake()
    {
        weaponBody = GetComponent<Rigidbody>();
        weaponColliders = GetComponents<Collider>();
        levitator = GetComponent<Levitator>();
        groundDetector = new WeaponGroundDetector();
    }

    private void Update()
    {
        if (isLevitating){
            levitator.Levitate();
        }
        else if (physicsIsEnabled){
            groundDetector.CheckIfGrounded(transform.position, groundCheckRadius);
        }
    }

    public void SubscribeOnCollision(Action<Collision> onCollisionQuerry) => this.onCollisionQuerry += onCollisionQuerry;
    public void SubscribeOnCollision(Action onCollision) => this.onCollision += onCollision;

    public void AddForce(Vector3 force) => weaponBody.AddForce(force);
    public void AddTorque(Vector3 torque) => weaponBody.AddTorque(torque);

    public void EnablePhysics()
    {
        physicsIsEnabled = true;

        ResetVelocity();
        SetColliders(true);
        SetKinematic(false);

        SetLevitation(false);
    }

    public void DisablePhysics()
    {
        physicsIsEnabled = false;

        ResetVelocity();
        SetColliders(false);
        SetKinematic(true);

        SetLevitation(false);

        groundDetector.RegisterIsNotGrounded();
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

    public void SubscribeOnGroundUpdate(Action<bool> onGroundUpdate) => groundDetector.SubscribeOnGroundUpdate(onGroundUpdate);
    public void UnsubscribeFromGroundUpdate(Action<bool> onGroundUpdate) => groundDetector.UnsubscribeFromGroundUpdate(onGroundUpdate);
}
