using UnityEngine;

public class Crate : MonoBehaviour, IOpenable, ICrate
{
    [SerializeField] private CratePhysics physics;
    [SerializeField] private CrateSFXPlayer sFXPlayer;
    [SerializeField] private MeshRenderer model;
    [SerializeField] private LaserBeam laserBeam;
    [SerializeField] private ParticleSystem destructionParticles;
    private string itemName;

    private bool isOpened;

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
        }
    }

    private void SwitchOff()
    {
        model.enabled = false;
        physics.SwitchOff();
        laserBeam.SwitchOff();
    }
}
