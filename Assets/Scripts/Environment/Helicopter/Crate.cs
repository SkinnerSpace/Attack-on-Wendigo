using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IOpenable
{
    [SerializeField] private Rigidbody physics;
    [SerializeField] private CrateSFXPlayer sFXPlayer;
    [SerializeField] private MeshRenderer model;
    [SerializeField] private LaserBeam laserBeam;
    [SerializeField] private ParticleSystem destructionParticles;
    private string itemName;

    private bool isOpened;

    public void ResetState()
    {
        isOpened = false;
        ResetPhysics();
    }

    public void Pack(string itemName) => this.itemName = itemName;

    public void Throw(float force)
    {
        physics.isKinematic = false;
        physics.useGravity = true;
        physics.AddForce(transform.forward * force, ForceMode.Impulse);

        sFXPlayer.PlayDrop();
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
        if (!isOpened){
            isOpened = true;

            model.enabled = false;
            laserBeam.SwitchOff();
            destructionParticles.Play();

            sFXPlayer.PlayOpen();
            sFXPlayer.PlayFlash();
            sFXPlayer.PlaySmoke();

            PoolHolder.Instance.SpawnFromThePool(itemName, transform.position, Quaternion.identity);
        }
    }
}
