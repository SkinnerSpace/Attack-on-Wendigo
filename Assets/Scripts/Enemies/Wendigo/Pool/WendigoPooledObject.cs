using UnityEngine;
using System;

public class WendigoPooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => gameObject;
    private IObjectPooler pooler;
    private Action onSpawn;

    private void Start() => pooler = PoolHolder.Instance;

    public void SubscribeOnSpawn(Action onSpawn) => this.onSpawn += onSpawn;

    public void OnObjectSpawn() => onSpawn?.Invoke();

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    public void BackToPool()
    {
        gameObject.SetActive(false);
        pooler.PutIntoThePool(this);
    }
}
