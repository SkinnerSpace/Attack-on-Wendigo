using UnityEngine;

public interface IPooledObject
{
    string PoolTag { get; set; }
    GameObject Object { get; }
    void OnObjectSpawn();
    void SetActive(bool active);
    void SetPositionAndRotation(Vector3 position, Quaternion rotation);
    void BackToPool();
}
