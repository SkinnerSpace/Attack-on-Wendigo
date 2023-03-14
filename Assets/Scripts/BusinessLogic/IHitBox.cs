public interface IHitBox : IDamageable
{
    void Subscribe(IDamageable damageable);
    void Unsubscribe(IDamageable damageable);
}
