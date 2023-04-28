using System;
using UnityEngine;

public class CratePooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }

    [SerializeField] private Crate crate;
    [SerializeField] private CrateLandingController landingController;
    [SerializeField] private CrateSweeper sweeper;
    [SerializeField] private LaserBeam laserBeam;
    [SerializeField] private Openable openable;

    private IObjectPooler pooler;

    public GameObject Object => gameObject;

    private void Awake(){
        crate.onTimeOutAfterUnpacking += BackToPool;
    }

    private void Start() => pooler = PoolHolder.Instance;

    public void OnObjectSpawn()
    {
        crate.ResetStateOnSpawn();
        landingController.ResetLanding();
        laserBeam.ResetLaserBeam();
        openable.ActivateCollision();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    public void BackToPool()
    {
        sweeper.SwitchOff();
        pooler.PutIntoThePool(this);
    }

    public void SubscribeOnSpawn(Action onSpawn) { }
}
