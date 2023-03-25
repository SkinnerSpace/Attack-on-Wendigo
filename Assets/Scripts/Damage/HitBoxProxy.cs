using UnityEngine;

public class HitBoxProxy : MonoBehaviour, IHitBox
{
    private HitBox hitBox;
    private Collider hitCollider;

    private void Awake()
    {
        hitCollider = GetComponent<Collider>();
    }

    public void ReceiveDamage(DamagePackage damagePackage) => hitBox.ReceiveDamage(damagePackage);

    public void Subscribe(IDamageable damageable)
    {
        if (hitBox == null)
            hitBox = new HitBox();

        hitBox.Subscribe(damageable);
    }

    public void Unsubscribe(IDamageable damageable) => hitBox.Unsubscribe(damageable);

    public void SwitchOn(){
        hitBox.SwitchOn();
        hitCollider.enabled = true;
    }

    public void SwitchOff(){
        hitBox.SwitchOff();
        hitCollider.enabled = false;
    }
}
