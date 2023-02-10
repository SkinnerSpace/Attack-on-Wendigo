using System.Collections.Generic;

public class PoolImplementor
{
    private IPoolObjectsKeeper poolObjectsKeeper;

    public PoolImplementor(IPoolObjectsKeeper poolObjectsKeeper) => this.poolObjectsKeeper = poolObjectsKeeper;

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

        pools.Add(poolData.tag, objectPool);
    }

    private void ExpandExisting(Dictionary<string, Queue<IPooledObject>> pools, PoolTemplate poolData)
    {
        Queue<IPooledObject> currentPool = pools[poolData.tag];
        FillThePool(currentPool, poolData);
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
