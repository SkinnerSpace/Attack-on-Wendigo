using System.Collections.Generic;
using UnityEngine;

public class PoolObjectSpawner
{
    public GameObject SpawnFromThePool(Dictionary<string, Queue<IPooledObject>> pools, PoolObjectOrder order)
    {
        if (!pools.ContainsKey(order.tag)) return null;

        IPooledObject objectToSpawn = TakeFromThePool(pools, order.tag);
        ActivateObject(objectToSpawn, order);
        PutIntoThePool(pools, objectToSpawn, order.tag);

        return objectToSpawn.Object;
    }

    private IPooledObject TakeFromThePool(Dictionary<string, Queue<IPooledObject>> pools, string tag) => pools[tag].Dequeue();

    private void PutIntoThePool(Dictionary<string, Queue<IPooledObject>> pools, IPooledObject obj, string tag) => pools[tag].Enqueue(obj);

    private void ActivateObject(IPooledObject obj, PoolObjectOrder order)
    {
        obj.SetPositionAndRotation(order.position, order.rotation);
        obj.SetActive(true);
        obj.OnObjectSpawn();
    }
}



