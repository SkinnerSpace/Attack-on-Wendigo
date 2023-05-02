using UnityEngine;
using System;
using WendigoCharacter;

public class WendigoPooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => gameObject;
    private IObjectPooler pooler;
    private Action onSpawn;
    private Action onBackToPool;

    [SerializeField] private RagDollController ragDollController;
    [SerializeField] private WendigoData data;
    [SerializeField] private CharacterController controller;

    private void Start() => pooler = PoolHolder.Instance;

    public void SubscribeOnSpawn(Action onSpawn) => this.onSpawn += onSpawn;
    public void SubscribeOnBackToPool(Action onBackToPool) => this.onBackToPool += onBackToPool;

    public void OnObjectSpawn()
    {
        onSpawn?.Invoke();
        ragDollController.ResetState();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        controller.enabled = true;
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    public void BackToPool()
    {
        onBackToPool?.Invoke();
        gameObject.SetActive(false);
        pooler.PutIntoThePool(this);
        data.ResetData();
    }
}
