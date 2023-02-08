using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Rigidbody physics;
    private GameObject item;

    public void Pack(GameObject item) => this.item = item;

    public void Throw(float force)
    {
        physics.isKinematic = false;
        physics.useGravity = true;
        physics.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
