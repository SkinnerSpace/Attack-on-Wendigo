using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PooledObjectsManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    private IPooledObject[] pooledObjects;
    public List<Transform> pooledObjectImps;

    private void OnEnable()
    {
        if (root != null){
            pooledObjects = root.GetComponentsInChildren<IPooledObject>();
            pooledObjectImps = new List<Transform>();

            foreach (IPooledObject pooledObject in pooledObjects)
            {
                pooledObjectImps.Add(pooledObject.Object.transform);
            }
        }
    }
}
