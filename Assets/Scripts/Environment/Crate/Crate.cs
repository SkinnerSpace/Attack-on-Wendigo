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

    private CargoDirector director;
    public event Action onTimeOutAfterUnpacking;

    private void Start()
    {
        director = CargoDirector.Instance;
    }

    public void ResetStateOnSpawn()
    {
        isOpened = false;
        ResetPhysics();
    }

    public void Pack(string itemName) => this.itemName = itemName;

    public void Interact(IInteractor interactor) => Open();

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
            Unpack();
            SpawnItem();
        }
    }

    public void Unpack()
    {
        isOpened = true;
        
        SwitchOff();
        destructionParticles.Play();
        sFXPlayer.PlayOpen();

        timer.Set("Unpacked", 2f, () => onTimeOutAfterUnpacking?.Invoke());
    }

    public void SpawnItem(){
        itemName = director.GetItem();
        PoolHolder.Instance.SpawnFromThePool(itemName, transform.position, Quaternion.identity);
    }

    private void SwitchOff()
    {
        model.enabled = false;
        physics.SwitchOff();
        laserBeam.SwitchOffAndLock();
    }
}
