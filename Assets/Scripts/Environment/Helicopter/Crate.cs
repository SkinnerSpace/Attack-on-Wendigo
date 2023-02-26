using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IOpenable
{
    [SerializeField] private Rigidbody physics;
    [SerializeField] private CrateSFXPlayer sfxPlayer;
    [SerializeField] private MeshRenderer model;
    [SerializeField] private LaserBeam laserBeam;
    private string itemName;

    private bool isOpened;

    private void Awake()
    {
        sfxPlayer = GetComponent<CrateSFXPlayer>();
    }

    public void Pack(string itemName) => this.itemName = itemName;

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

    public void Open()
    {
        if (!isOpened)
        {
            model.enabled = false;
            laserBeam.SwitchOff();
            isOpened = true;
            PoolHolder.Instance.SpawnFromThePool(itemName, transform.position, Quaternion.identity);
        }
    }
}
