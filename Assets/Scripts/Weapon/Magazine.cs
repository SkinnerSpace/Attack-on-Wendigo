using System;
using UnityEngine;

public class Magazine
{
    private WeaponData data;
    private FunctionTimer timer;

    private bool ableToReport = true;

    public event Action<int> onUpdate;
    private event Action onEmpty;

    public Magazine(WeaponData data, FunctionTimer timer)
    {
        this.data = data;
        this.timer = timer;
    }

    public void OnReady(bool isReady)
    {
        if (isReady) Subscribe(AmmoBar.Instance);
        else Unsubscribe(AmmoBar.Instance);
    }

    public void SubscribeOnEmpty(Action onEmpty) => this.onEmpty += onEmpty;

    public void NotifyOnEmpty()
    {
        if (ableToReport)
        {
            ableToReport = false;
            AmmoBar.Instance.UpdateAmmo(0);
            onEmpty?.Invoke();

            timer.Set("EnableReport", 0.5f, () => ableToReport = true);
        }
    }

    public void Subscribe(IAmmoObserver observer)
    {
        observer.SetActive(true);
        onUpdate += observer.UpdateAmmo;
        
        onUpdate?.Invoke(data.Ammo);
    }

    public void Unsubscribe(IAmmoObserver observer)
    {
        observer.SetActive(false);
        onUpdate -= observer.UpdateAmmo;
    }

    public bool HasAmmo() => data.Ammo > 0;

    public bool IsEmpty() => data.Ammo <= 0;

    public void ReduceCount()
    {
        data.SetAmmo(data.Ammo - 1);
        onUpdate?.Invoke(data.Ammo);
    }

    public void RestoreAmmo() => data.SetAmmo(data.AmmoCapacity);
}
