public interface IPoolObjectsKeeper
{
    IPooledObject CreatePooledObject(PoolTemplate poolData);
}
