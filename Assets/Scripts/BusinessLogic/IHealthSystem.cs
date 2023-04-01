using System;

public interface IHealthSystem
{
    void SubscribeOnDeath(Action onDeath);
}
