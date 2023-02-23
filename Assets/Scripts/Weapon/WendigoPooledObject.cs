using UnityEngine;
using System;

public class WendigoPooledObject : MonoBehaviour, IPooledObject
{
    public GameObject Object => gameObject;
    private Action onSpawn;

    public void Subscribe(IPooledObjectObserver observer) => onSpawn += observer.OnSpawn;

    public void OnObjectSpawn() => onSpawn?.Invoke();

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);
}
