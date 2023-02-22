using System;

public abstract class BaseController
{
    public Type Type => GetType();
    public abstract void Initialize(MainController main);
    public abstract void Connect();
    //public abstract void Disconnect();
}

public interface IBaseController
{
    void Initialize(MainController main);
    void Connect();
}
