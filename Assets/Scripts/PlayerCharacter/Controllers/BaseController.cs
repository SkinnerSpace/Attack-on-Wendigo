using System;

public abstract class BaseController
{
    public abstract void Initialize(MainController main);
    public abstract void Connect();
    public abstract void Disconnect();
}
