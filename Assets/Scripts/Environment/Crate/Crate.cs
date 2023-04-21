using System;
using UnityEngine;

public class Crate : MonoBehaviour, IOpenable, ICrate
{
    [SerializeField] private CratePhysics physics;
    [SerializeField] private CrateSFXPlayer sFXPlayer;
    [SerializeField] private MeshRenderer model;
    [SerializeField] private LaserBeam laserBeam;
    [SerializeField] private ParticleSystem destructionParticles;
    [SerializeField] private FunctionTimer timer;
    private string itemName;

    private bool isOpened;

    public event Action onOpen;

    public void ResetStateOnSpawn()
    {
        isOpened = false;
        ResetPhysics();
    }

    public void Pack(string itemName) => this.itemName = itemName;

    public void Throw(float force)
    {
        physics.SwitchOn();
        physics.AddForce(force);

        sFXPlayer.PlayDrop();
    }

    public void ResetPhysics() => physics.SetKinematic();

    public void Open()
    {
        if (!isOpened){
            isOpened = true;

            SwitchOff();
            destructionParticles.Play();
            sFXPlayer.PlayOpen();

            PoolHolder.Instance.SpawnFromThePool(itemName, transform.position, Quaternion.identity);
            timer.Set("Open", 2f, () => onOpen?.Invoke());
        }
    }

    public void OpenEmpty()
    {
        if (!isOpened){
            isOpened = true;

            SwitchOff();
            destructionParticles.Play();

            GameEvents.current.WeaponHasBeenSweptAway();
            timer.Set("Open", 2f, () => onOpen?.Invoke());
        }
    }

    private void SwitchOff()
    {
        model.enabled = false;
        physics.SwitchOff();
        laserBeam.SwitchOff();
    }
}
