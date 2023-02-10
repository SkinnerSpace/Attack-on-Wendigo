using UnityEngine;

public interface IPooledObject
{
    GameObject Object { get; }
    void OnObjectSpawn();
    void SetActive(bool active);
    void SetPositionAndRotation(Vector3 position, Quaternion rotation);
}
