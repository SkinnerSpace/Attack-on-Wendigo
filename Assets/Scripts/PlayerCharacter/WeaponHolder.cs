using System;
using UnityEngine;

public class WeaponHolder : MonoBehaviour, IHolder
{
    [SerializeField] Shooter shooter;
    [SerializeField] Speedometer speedometer;

    public Vector3 targetPosition =>  weapon.DefaultPosition;

    private IWeapon weapon;
    public Action onPickedUp;

    private void Awake()
    {
        weapon = NullWeapon.Instance;
        onPickedUp += WeaponIsTaken;
    }

    public void TakeAnItem(Transform item)
    {
        SetWeapon(item);
        PickUp(item);
        ConnectSpeedometerTo(item);
    }

    private void SetWeapon(Transform item)
    {
        weapon = item.GetComponent<IWeapon>();
        shooter.SetWeapon(weapon);
    }
    private void PickUp(Transform item) => item.GetComponent<IPickable>().PickUp(this, onPickedUp);
    private void ConnectSpeedometerTo(Transform item) => item.GetComponent<ISpeedObserver>().ConnectSpeedometer(speedometer);
    private void WeaponIsTaken() => weapon.GetReady(true);
}
