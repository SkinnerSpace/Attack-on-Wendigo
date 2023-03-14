using System.Collections.Generic;
using UnityEngine;

public class PoolImplementor
{
    private IPoolObjectsKeeper poolObjectsKeeper;
    private Dictionary<string, PoolTemplate> templates;

    public PoolImplementor(IPoolObjectsKeeper poolObjectsKeeper)
    {
        this.poolObjectsKeeper = poolObjectsKeeper;
        templates = new Dictionary<string, PoolTemplate>();
    }

    public void ImplementThePool(Dictionary<string, Queue<IPooledObject>> pools, PoolTemplate poolData)
    {
        if (!pools.ContainsKey(poolData.tag))
        {
            CreateNewObjectPool(pools, poolData);
        }
        else
        {
            ExpandExisting(pools, poolData);
        }
    }

    private void CreateNewObjectPool(Dictionary<string, Queue<IPooledObject>> pools, PoolTemplate poolData)
    {
        Queue<IPooledObject> objectPool = new Queue<IPooledObject>();
        FillThePool(objectPool, poolData);

        templates.Add(poolData.tag, poolData);
        pools.Add(poolData.tag, objectPool);
    }

    private void ExpandExisting(Dictionary<string, Queue<IPooledObject>> pools, PoolTemplate poolData)
    {
        Queue<IPooledObject> currentPool = pools[poolData.tag];
        FillThePool(currentPool, poolData);
    }

    public void ExpandThePoolByOne(Dictionary<string, Queue<IPooledObject>> pools, string poolTag)
    {
        PoolTemplate poolData = templates[poolTag];
        poolData.size += 1;

        Queue<IPooledObject> currentPool = pools[poolTag];
        FillThePool(currentPool, poolData);

        Debug.Log("Pool has been expanded, shame on me! " + poolTag);
    }

    private void FillThePool(Queue<IPooledObject> objectPool, PoolTemplate poolData)
    {
        int count = poolData.size - objectPool.Count;

        for (int i = 0; i < count; i++)
        {
            IPooledObject obj = poolObjectsKeeper.CreatePooledObject(poolData);
            objectPool.Enqueue(obj);
        }
    }
}
