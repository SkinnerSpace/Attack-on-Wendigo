using System;
using UnityEngine;

public class HitBoxProxy : MonoBehaviour, IHitBox
{
    public Transform owner;
    private HitBox hitBox;
    private Collider hitCollider;

    private int joints;

    private event Action onGetDamage;

    private void Awake()
    {
        hitCollider = GetComponent<Collider>();
    }

    public void SetOwner(Transform owner) => this.owner = owner;

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        hitBox.ReceiveDamage(damagePackage);
        onGetDamage?.Invoke();
    }

    public void Subscribe(IDamageable damageable)
    {
        if (hitBox == null)
            hitBox = new HitBox();

        hitBox.Subscribe(damageable);
    }

    public void Unsubscribe(IDamageable damageable) => hitBox.Unsubscribe(damageable);

    public void SubscribeOnGettingDamage(Action onGetDamage) => this.onGetDamage += onGetDamage;

    public void SwitchOn(){
        hitBox.SwitchOn();
        hitCollider.enabled = true;
    }

    public void IncrementJoints(int count) => joints += count;

    public void SwitchOff()
    {
        joints -= 1;

        if (joints <= 0){
            hitBox.SwitchOff();
            hitCollider.enabled = false;
        }
    }
}

