using System;
using UnityEngine;

public class EnergyShieldPooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }

    [SerializeField] private EnergyShield shield;

    private IObjectPooler pooler;

    public GameObject Object => gameObject;

    private void Awake(){
        shield.onDisappear += BackToPool;
    }

    private void Start() => pooler = PoolHolder.Instance;

    public void BackToPool()
    {
        pooler.PutIntoThePool(this);
    }

    public void OnObjectSpawn(){
        shield.OnObjectSpawn();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    public void SubscribeOnSpawn(Action onSpawn) { }
}
