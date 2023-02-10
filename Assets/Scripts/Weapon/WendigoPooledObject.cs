using UnityEngine;

public class WendigoPooledObject : MonoBehaviour, IPooledObject
{
    public GameObject Object => gameObject;

    public void OnObjectSpawn()
    {

    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);
}

