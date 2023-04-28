using System;

public interface IHealthSystem
{
    void RestoreHealth(int health);
    void SubscribeOnDeath(Action onDeath);
}
