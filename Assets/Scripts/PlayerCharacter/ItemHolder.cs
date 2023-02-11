using System;
using UnityEngine;

public class ItemHolder : MonoBehaviour, IHolder
{
    private const float dropForce = 1000f;

    [SerializeField] WeaponHandler weaponHandler;
    [SerializeField] Speedometer speedometer;
    [SerializeField] PlayerVision vision;
    private Camera cam;

    public Vector3 targetPosition =>  weapon.DefaultPosition;

    private Transform item;
    private IPickable pickable;
    private IWeapon weapon;

    public Action onPickedUp;

    private void Awake()
    {
        cam = GetComponent<Camera>();
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
            DropARedundantItem();

            this.item = item;
            SetWeapon(item);
            PickUp(item);
        }
    }

    private void DropARedundantItem()
    {
        if (item != null){
            DropAnItem();
        }
    }

    private void DropAnItem()
    {
        if (OldInputReader.interact && weapon.isReady)
        {
            Vector3 dropVelocity = GetDropVelocity();
            ResetWeapon();
            pickable.Drop(dropVelocity);
        }
    }

    private void SetWeapon(Transform item)
    {
        weapon = item.GetComponent<IWeapon>();
        weaponHandler.SetWeapon(weapon);

        ConnectSpeedometerTo(item);
        ConnectCameraTo(item);
    }

    private void ResetWeapon()
    {
        item = null;
        weapon.SetReady(false);
        weapon = NullWeapon.Instance;
        weaponHandler.ResetWeapon();
    }

    private void PickUp(Transform item)
    {
        pickable = item.GetComponent<IPickable>();
        pickable.PickUp(this, onPickedUp);
    }

    private void ConnectSpeedometerTo(Transform item) => item.GetComponent<ISpeedObserver>().ConnectSpeedometer(speedometer);
    private void ConnectCameraTo(Transform item) => item.GetComponent<ICameraUser>().ConnectCamera(cam);
    private void WeaponIsTaken() => weapon.SetReady(true);
    private Vector3 GetDropVelocity() => GetDropDirection() * dropForce;
    private Vector3 GetDropDirection() => (vision.point - item.position).normalized;
}
