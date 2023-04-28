using System;
using UnityEngine;

public class Magazine
{
    private WeaponData data;
    private FunctionTimer timer;

    private bool ableToReport = true;

    public event Action onOutOfAmmo;

    public Magazine(WeaponData data, FunctionTimer timer)
    {
        this.data = data;
        this.timer = timer;
    }

    public void NotifyOnEmpty()
    {
        if (ableToReport)
        {
            ableToReport = false;
            PlayerEvents.current.UpdateAmmo(data.Ammo);
            onOutOfAmmo?.Invoke();

            timer.Set("EnableReport", 0.5f, () => ableToReport = true);
        }
    }

    public bool HasAmmo() => data.Ammo > 0;

    public bool IsEmpty() => data.Ammo <= 0;

    public void ReduceCount()
    {
        data.SetAmmo(data.Ammo - 1);
        PlayerEvents.current.UpdateAmmo(data.Ammo);
    }

    public void RestoreAmmo() => data.SetAmmo(data.AmmoCapacity);
}
