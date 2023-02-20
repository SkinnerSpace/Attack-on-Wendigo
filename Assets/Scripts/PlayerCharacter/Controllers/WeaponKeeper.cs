using System.Collections;
using System;
using UnityEngine;
using UnityEditor;

public class WeaponKeeper : BaseController, IKeeper, IInteractor, IMousePosObserver
{
    private MainController main;
    private ICharacterData data;
    public Transform Root => data.Cam.transform;

    private Weapon weapon;
    private WeaponThrower thrower;

    private Vector3 dropPos;

    public override void Initialize(MainController main)
    {
        this.main = main;
        data = main.Data;

        thrower = new WeaponThrower(data);
    }

    public override void Connect()
    {
        MainInputReader.Get<InteractionInputReader>().Subscribe(this);
        MainInputReader.Get<MousePositionInputReader>().Subscribe(this);
    }

    public void TakeAnItem(Transform item)
    {
        Weapon takenWeapon = item.GetComponent<Weapon>();

        if (takenWeapon != weapon)
        {
            weapon = takenWeapon;
            weapon.PickUp(this, CallMeBack);
        }
    }

    public void Interact() => DropAnItem();

    public void DropAnItem()
    {
        if (weapon != null)
        {
            Vector3 force = data.CameraForward * data.DropItemStrength;
            weapon.Drop(dropPos, force);
            weapon = null;
        }
    }

    private void CallMeBack() => Debug.Log("Item PICKED UP!");

    public void OnMousePosUpdate(Vector2 screenPoint) => dropPos = thrower.GetDropPos(weapon, screenPoint);
}
