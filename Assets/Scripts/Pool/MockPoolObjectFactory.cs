public class MockPoolObjectFactory: IPoolObjectsKeeper
{
    public IPooledObject CreatePooledObject(PoolTemplate poolData) => new MockPooledObject();
}
