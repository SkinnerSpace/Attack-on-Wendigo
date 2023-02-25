using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Rigidbody physics;
    [SerializeField] private CrateSFXPlayer sfxPlayer;
    private GameObject item;

    private void Awake()
    {
        sfxPlayer = GetComponent<CrateSFXPlayer>();
    }

    public void Pack(GameObject item) => this.item = item;

    public void Throw(float force)
    {
        physics.isKinematic = false;
        physics.useGravity = true;
        physics.AddForce(transform.forward * force, ForceMode.Impulse);

        sfxPlayer.PlayDropSFX();
    }

    public void ResetPhysics()
    {
        physics.isKinematic = true;
        physics.useGravity = false;
        physics.velocity = Vector3.zero;
    }

    public void PrepareToBeUnpacked()
    {
        ResetPhysics();
    }
}
