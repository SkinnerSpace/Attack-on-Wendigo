using System;
using UnityEngine;

public class CharacterHealthSystem : BaseController, IDamageable
{
    private MainController main;
    private ICharacterData data;
    private HitBox hitBox;
    private EventManager eventManager;

    public int Health => data.Health;
    public bool IsAlive => Health > 0;

    private EventTrigger onDeathTrigger;

    private event Action<int> onHealthUpdate;
    private event Action onDeath;
    private event Action<float> onImpact;

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        if (IsAlive)
        {
            data.Health -= damagePackage.damage;

            if (data.Health <= 0)
                Die();

            onHealthUpdate?.Invoke(data.Health);

            data.AddVelocity(damagePackage.impact);
            onImpact?.Invoke(damagePackage.impact.magnitude);
        }
    }

    public void SubscribeOnUpdate(Action<int> onHealthUpdate) => this.onHealthUpdate += onHealthUpdate;
    public void SubscribeOnDeath(Action onDeath) => this.onDeath += onDeath;
    public void SubscribeOnImpact(Action<float> onImpact) => this.onImpact += onImpact;
    public void UnsubscribeFromImpact(Action<float> onImpact) => this.onImpact -= onImpact;

    public override void Initialize(MainController main)
    {
        this.main = main;
        data = main.Data;
        hitBox = main.HitBox;
        eventManager = main.Events;

        onDeathTrigger = new EventTrigger();
        SubscribeOnDeath(() => onDeathTrigger.SetActive(true));

        SubscribeOnUpdate(HealthBar.Instance.OnUpdate);
    }

    public void Initialize(ICharacterData data) => this.data = data;

    public override void Connect()
    {
        hitBox.Subscribe(this);
        SubscribeOnDeath(() => main.SetActive(false));
        eventManager.ConnectTrigger(onDeathTrigger, "PlayerDied");
    }

    public override void Disconnect() => hitBox.Unsubscribe(this);

    public void Die()
    {
        data.Health = 0;
        onDeath?.Invoke();
    }
}

