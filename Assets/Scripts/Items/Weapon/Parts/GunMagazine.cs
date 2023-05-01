using System;
using UnityEngine;

public class GunMagazine : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private int capacity = 10;
    private int ammo = 10;

    [SerializeField] private float reloadTime = 1f;
    private bool isReloading;

    private FunctionTimer timer;
    public Action<int> updateAmmo;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
        ammo = capacity;
    }

    private void Start()
    {

        updateAmmo?.Invoke(ammo);
    }

    public GameObject TakeAmmo()
    {
        SubtractAmmo();
        return bullet;
    }

    private void SubtractAmmo()
    {
        ammo -= 1;
        updateAmmo?.Invoke(ammo);
    }

    public bool IsEmpty()
    {
        return ammo <= 0;
    }

    public void Reload()
    {
        if (!isReloading && ammo < capacity)
        {
            isReloading = true;
            timer.Set("Reload", reloadTime, RestoreAmmo);
        }
    }

    public void RestoreAmmo() => ammo = capacity;
}