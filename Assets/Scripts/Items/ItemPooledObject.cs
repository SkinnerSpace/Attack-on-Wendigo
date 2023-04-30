using System;
using UnityEngine;

public abstract class ItemPooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => gameObject;

    private Pickable pickable;
    private Levitator levitator;
    private AbandonmentDetector abandonmentDetector;

    private IObjectPooler pooler;

    protected virtual void Awake()
    {
        pickable = GetComponentInChildren<Pickable>();
        levitator = GetComponentInChildren<Levitator>();
        abandonmentDetector = GetComponentInChildren<AbandonmentDetector>();
    }

    private void Start() => pooler = PoolHolder.Instance;

    public virtual void OnObjectSpawn()
    {
        float randomAngle = Rand.Range(0f, 360f);
        Vector3 randomEuler = new Vector3(0f, randomAngle, 0f);
        Quaternion randomRotation = Quaternion.Euler(randomEuler);
        transform.rotation = randomRotation;

        pickable.SwitchOn();
        levitator.SwitchOn();
        abandonmentDetector.ResetState();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    public void BackToPool()
    {
        pickable.ResetState();
        pooler.PutIntoThePool(this);
    }

    public void SubscribeOnSpawn(Action onSpawn) { }
}
