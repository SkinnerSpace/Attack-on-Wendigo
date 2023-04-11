using System;

public interface ISpawner
{
    void SubscribeOnAliveCountUpdate(Action<int> onCountUpdate);
}