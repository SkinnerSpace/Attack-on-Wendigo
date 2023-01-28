using System;
using UnityEngine;

public class ItemHolder : MonoBehaviour, IHolder
{
    private const float dropForce = 1000f;

    [SerializeField] WeaponHandler weaponHandler;
    [SerializeField] Speedometer speedometer;
    [SerializeField] PlayerVision vision;

    public Vector3 targetPosition =>  weapon.DefaultPosition;

    private Transform item;
    private IPickable pickable;
    private IWeapon weapon;

    public Action onPickedUp;

    private void Awake()
    {
        weapon = NullWeapon.Instance;
        onPickedUp += WeaponIsTaken;
    }

    private void Update()
    {
        DropAnItem();
    }

    public void TakeAnItem(Transform item)
    {
        if (this.item != item){
            this.item = item;

            SetWeapon(item);
            PickUp(item);
            ConnectSpeedometerTo(item);
            ConnectVisionTo(item);
        }
    }

    private void DropAnItem()
    {
        if (InputReader.interact && weapon.isReady)
        {
            Vector3 dropVelocity = GetDropVelocity();
            ResetWeapon();
            pickable.Drop(dropVelocity);
        }
    }

    private void ResetWeapon()
    {
        item = null;
        weapon.GetReady(false);
        weapon = NullWeapon.Instance;
        weaponHandler.ResetWeapon();
    }

    private void SetWeapon(Transform item)
    {
        weapon = item.GetComponent<IWeapon>();
        weaponHandler.SetWeapon(weapon);
    }

    private void PickUp(Transform item)
    {
        pickable = item.GetComponent<IPickable>();
        pickable.PickUp(this, onPickedUp);
    }

    private void ConnectSpeedometerTo(Transform item) => item.GetComponent<ISpeedObserver>().ConnectSpeedometer(speedometer);
    private void ConnectVisionTo(Transform item) => item.GetComponent<IVisionUser>().ConnectVision(vision);
    private void WeaponIsTaken() => weapon.GetReady(true);
    private Vector3 GetDropVelocity() => GetDropDirection() * dropForce;
    private Vector3 GetDropDirection() => (vision.point - item.position).normalized;
}
