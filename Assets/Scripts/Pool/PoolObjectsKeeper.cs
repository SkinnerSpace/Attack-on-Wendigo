using UnityEngine;

public class PoolObjectsKeeper : IPoolObjectsKeeper
{
    private PoolObjectsNestedContainer nestedContainer;

    public PoolObjectsKeeper(PoolObjectsNestedContainer nestedContainer) => this.nestedContainer = nestedContainer;

    public IPooledObject CreatePooledObject(PoolTemplate poolData)
    {
        Transform container = nestedContainer.GetContainer(poolData.tag);
        IPooledObject obj = Object.Instantiate(poolData.prefab, container).GetComponent<IPooledObject>();
        obj.SetActive(false);

        return obj;
    }
}
