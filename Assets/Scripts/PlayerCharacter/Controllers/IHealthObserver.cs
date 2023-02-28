public interface IHealthObserver
{
    void OnHealthUpdate(int health);
    void OnDeath();
}
