using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemPhysicalBody : MonoBehaviour, IPhysicalBody
{
    [SerializeField] private float groundCheckRadius = 1f;
    [SerializeField] private bool visualizeCollider;

    private Rigidbody weaponBody;
    private Collider[] combinedColliders;
    private ItemGroundDetector groundDetector;

    public Vector3 Velocity => weaponBody.velocity;

    private bool physicsIsEnabled;

    public event Action<Collision> onCollisionQuerry;
    public event Action onCollision;
    public event Action onDisabled;
    
    private void Awake()
    {
        weaponBody = GetComponent<Rigidbody>();
        combinedColliders = GetComponents<Collider>();
        groundDetector = new ItemGroundDetector();
    }

    private void Update()
    {
        if (physicsIsEnabled){
            groundDetector.CheckIfGrounded(transform.position, groundCheckRadius);
        }
    }

    public void AddForce(Vector3 force) => weaponBody.AddForce(force);
    public void AddTorque(Vector3 torque) => weaponBody.AddTorque(torque);

    public void EnablePhysics()
    {
        physicsIsEnabled = true;

        ResetVelocity();
        SetColliders(true);
        SetKinematic(false);
    }

    public void DisablePhysics()
    {
        physicsIsEnabled = false;
        onDisabled?.Invoke();

        ResetVelocity();
        SetColliders(false);
        SetKinematic(true);

        groundDetector.RegisterIsNotGrounded();
    }

    private void ResetVelocity() => weaponBody.velocity = Vector3.zero;

    private void SetColliders(bool enabled)
    {
        foreach (Collider combinedCollider in combinedColliders){
            combinedCollider.enabled = enabled;
        }
    }

    private void SetKinematic(bool enabled)
    {
        weaponBody.isKinematic = enabled;
        weaponBody.useGravity = !enabled;
    }

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionQuerry?.Invoke(collision);
        onCollision?.Invoke();
    }

    public void SubscribeOnGroundUpdate(Action<bool> onGroundUpdate) => groundDetector.onGroundUpdate += onGroundUpdate;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (visualizeCollider)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
            Gizmos.color = Color.white;
        }
    }
# endif
}
