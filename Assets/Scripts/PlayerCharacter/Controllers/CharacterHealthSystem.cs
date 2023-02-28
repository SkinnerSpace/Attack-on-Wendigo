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

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        if (IsAlive)
        {
            data.Health -= damagePackage.damage;

            onHealthUpdate?.Invoke(data.Health);

            if (data.Health <= 0)
                Die();
        }
    }

    public void SubscribeOnUpdate(Action<int> onHealthUpdate) => this.onHealthUpdate += onHealthUpdate;
    public void SubscribeOnDeath(Action onDeath) => this.onDeath += onDeath;

    public override void Initialize(MainController main)
    {
        this.main = main;
        hitBox = main.HitBox;
        Initialize(main.Data);
        eventManager = main.Events;

        onDeathTrigger = new EventTrigger();

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

    private void Die()
    {
        onDeath?.Invoke();
        onDeathTrigger.SetActive(true);
    }
}

