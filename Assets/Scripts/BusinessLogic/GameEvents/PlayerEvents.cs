using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents current;

    public event Action onDeath;
    public event Action<int> onHealthUpdate;

    public event Action<int> onAmmoUpdate;
    public event Action onWeaponThrown;

    public event Action<RaycastHit> onInteractiveTargetAdded;
    public event Action onInteractiveTargetRemoved;

    private void Awake(){
        current = this;
    }

    public void Die() => onDeath?.Invoke();
    public void UpdateHealth(int health) => onHealthUpdate?.Invoke(health);

    public void UpdateAmmo(int ammo) => onAmmoUpdate?.Invoke(ammo);
    public void ThrowWeapon() => onWeaponThrown?.Invoke();

    public void AddInteractiveTarget(RaycastHit interactiveTarget) => onInteractiveTargetAdded?.Invoke(interactiveTarget);
    public void RemoveInteractiveTarget() => onInteractiveTargetRemoved?.Invoke();
}