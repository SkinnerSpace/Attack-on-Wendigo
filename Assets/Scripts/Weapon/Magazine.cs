using System;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private int ammo;

    public int Ammo => ammo;

    public event Action<bool> notifyOnActive;
    public event Action<int> notifyOnUpdate;
    public event Action notifyOutOfAmmo;

    public void GetReady(bool isReady)
    {
        if (isReady) Subscribe();
        else Unsubscribe();
    }

    private void Subscribe()
    {
        notifyOnActive += AmmoBar.Instance.SetActive;
        notifyOnUpdate += AmmoBar.Instance.UpdateAmmo;
        notifyOutOfAmmo += AmmoBar.Instance.UpdateOutOfAmmo;

        notifyOnActive?.Invoke(true);
        notifyOnUpdate?.Invoke(ammo);
    }

    private void Unsubscribe()
    {
        notifyOnActive?.Invoke(false);

        notifyOnUpdate -= AmmoBar.Instance.UpdateAmmo;
        notifyOutOfAmmo -= AmmoBar.Instance.UpdateOutOfAmmo;
    }

    public bool HasAmmo()
    {
        if (ammo > 0) return true;

        return false;
    }

    public void ReduceCount()
    {
        ammo -= 1;
        notifyOnUpdate(ammo);
    }
}