using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : IObjectPooler
{
    private Dictionary<string, Queue<IPooledObject>> pools = new Dictionary<string, Queue<IPooledObject>>();
    private List<IPooledObject> disposedObjects = new List<IPooledObject>();

    private PoolImplementor implementor;
    private PoolObjectSpawner spawner;

    public ObjectPooler(PoolImplementor implementor, PoolObjectSpawner spawner)
    {
        pools = new Dictionary<string, Queue<IPooledObject>>();
        this.implementor = implementor;
        this.spawner = spawner;
    }

    public void ExecutePoolTemplates(List<PoolTemplate> poolTemplates)
    {
        foreach (PoolTemplate poolData in poolTemplates)
            implementor.ImplementThePool(pools, poolData);
    }

    public GameObject SpawnFromThePool(string tag, Vector3 position, Quaternion rotation)
    {
        PoolObjectOrder order = new PoolObjectOrder(tag, position, rotation);
        return Spawn(order);
    }

    public GameObject SpawnFromThePool(string tag)
    {
        PoolObjectOrder order = new PoolObjectOrder(tag, Vector3.zero, Quaternion.identity);
        return Spawn(order);
    }

    private GameObject Spawn(PoolObjectOrder order)
    {
        if (pools.ContainsKey(order.tag))
        {
            if (pools[order.tag].Count <= 0)
                implementor.ExpandThePoolByOne(pools, order.tag);

            return spawner.SpawnFromThePool(pools, disposedObjects, order);
        }

        return null;
    }

    public void PutIntoThePool(IPooledObject obj)
    {
        if (!string.IsNullOrEmpty(obj.PoolTag))
        {
            pools[obj.PoolTag].Enqueue(obj);
            disposedObjects.Remove(obj);
        }
    }
}



