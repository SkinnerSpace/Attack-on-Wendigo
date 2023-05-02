using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents current;

    public event Action onDeath;
    public event Action onHammered;
    public event Action onBurnt;
    public event Action<int> onHealthUpdate;
    public event Action onHealthRestore;

    public event Action<int> onAmmoUpdate;
    public event Action onWeaponThrown;

    public event Action<RaycastHit> onInteractiveTargetAdded;
    public event Action onInteractiveTargetRemoved;

    public event Action onReceivedBluntDamage;

    private void Awake(){
        current = this;
    }

    public void NotifyOnDeath() => onDeath?.Invoke();
    public void NotifyOnHammered() => onHammered?.Invoke();
    public void NotifyOnBurnt() => onBurnt?.Invoke();
    public void NotifyOnHealthUpdate(int health) => onHealthUpdate?.Invoke(health);
    public void NotifyOnHealthRestore() => onHealthRestore?.Invoke();

    public void NotifyOnAmmoUpdate(int ammo) => onAmmoUpdate?.Invoke(ammo);
    public void ThrowWeapon() => onWeaponThrown?.Invoke();

    public void AddInteractiveTarget(RaycastHit interactiveTarget) => onInteractiveTargetAdded?.Invoke(interactiveTarget);
    public void RemoveInteractiveTarget() => onInteractiveTargetRemoved?.Invoke();

    public void NotifyOnReceivedBluntDamage() => onReceivedBluntDamage?.Invoke();
}