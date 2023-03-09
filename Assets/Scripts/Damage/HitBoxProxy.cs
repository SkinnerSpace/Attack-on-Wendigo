using UnityEngine;

public class HitBoxProxy : MonoBehaviour, IHitBox
{
    private HitBox hitBox;

    public void Subscribe(IDamageable damageable)
    {
        if (hitBox == null)
            hitBox = new HitBox();

        hitBox.Subscribe(damageable);
    }

    public void Unsubscribe(IDamageable damageable) => hitBox.Unsubscribe(damageable);

    public void ReceiveDamage(DamagePackage damagePackage) => hitBox.ReceiveDamage(damagePackage);
}
