using System;

public interface ISpawner
{
    void SubscribeOnCountUpdate(Action<int> onCountUpdate);
}