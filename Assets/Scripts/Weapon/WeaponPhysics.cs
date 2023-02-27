﻿using System;
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

    public void SetPhysicsDisabled(bool disabled)
    {
        weaponBody.velocity = Vector3.zero;

        foreach (Collider weaponCollider in weaponColliders)
            weaponCollider.enabled = !disabled;

        weaponBody.isKinematic = disabled;
        weaponBody.useGravity = !disabled;
    }

    public void SetLevitation(bool isLevitating) => this.isLevitating = isLevitating;

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionQuerry?.Invoke(collision);
        onCollision?.Invoke();
    }
}
