using System;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private WeaponSFXPlayer sfxPlayer;
    [SerializeField] private WeaponData data;

    private FunctionTimer timer;
    private AmmoReporter reporter;

    public event Action<int> onUpdate;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
        reporter = new AmmoReporter(timer, sfxPlayer);
    }

    public void GetReady(bool isReady)
    {
        if (isReady) Subscribe(AmmoBar.Instance);
        else Unsubscribe(AmmoBar.Instance);
    }

    public void ReportIsEmpty() => reporter.ReportIsEmpty();

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
        onUpdate(data.Ammo);
    }
}
