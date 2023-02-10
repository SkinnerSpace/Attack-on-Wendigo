using System.Collections.Generic;
using UnityEngine;

public class PoolHolder : Singleton<PoolHolder>, IObjectPooler
{
    private ObjectPooler objectPooler;

    protected override void Awake()
    {
        base.Awake();

        PoolObjectsNestedContainer nestedContainer = new PoolObjectsNestedContainer(transform);
        PoolObjectsKeeper poolObjectsKeeper = new PoolObjectsKeeper(nestedContainer);
        PoolImplementor implementor = new PoolImplementor(poolObjectsKeeper);
        PoolObjectSpawner spawner = new PoolObjectSpawner();

        objectPooler = new ObjectPooler(implementor, spawner);
    }

    public void ExecutePoolTemplates(List<PoolTemplate> poolTemplates) => objectPooler.ExecutePoolTemplates(poolTemplates);

    public GameObject SpawnFromThePool(string tag, Vector3 position, Quaternion rotation) => objectPooler.SpawnFromThePool(tag, position, rotation);
    public GameObject SpawnFromThePool(string tag) => objectPooler.SpawnFromThePool(tag);
}
