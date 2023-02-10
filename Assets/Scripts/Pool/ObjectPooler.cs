using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : IObjectPooler
{
    [SerializeField] private Dictionary<string, Queue<IPooledObject>> pools = new Dictionary<string, Queue<IPooledObject>>();
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
        return spawner.SpawnFromThePool(pools, order);
    }

    public GameObject SpawnFromThePool(string tag)
    {
        PoolObjectOrder order = new PoolObjectOrder(tag, Vector3.zero, Quaternion.identity);
        return spawner.SpawnFromThePool(pools, order);
    }
}



