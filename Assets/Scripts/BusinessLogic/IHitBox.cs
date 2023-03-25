public interface IHitBox : IDamageable, ISwitchable
{
    void Subscribe(IDamageable damageable);
    void Unsubscribe(IDamageable damageable);
}
