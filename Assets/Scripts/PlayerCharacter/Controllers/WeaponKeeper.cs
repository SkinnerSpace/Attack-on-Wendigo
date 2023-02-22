using System.Collections;
using System;
using UnityEngine;
using UnityEditor;

public class WeaponKeeper : BaseController, IKeeper, IInteractor, IMousePosObserver
{
    private ICharacterData data;
    public Transform Root => data.Cam.transform;

    private IPickable item;
    private Weapon weapon;
    private WeaponThrower thrower;

    private Vector3 dropPos;

    public override void Initialize(MainController main)
    {
        data = main.Data;
        thrower = new WeaponThrower(data);
    }

    public override void Connect()
    {
        MainInputReader.Get<InteractionInputReader>().Subscribe(this);
        MainInputReader.Get<MousePositionInputReader>().Subscribe(this);
    }

    public void TakeAnItem(IPickable item)
    {
        if (this.item != item)
        {
            this.item = item;
            weapon = item.Get<Weapon>();
            item.PickUp(this, OnKept);
            weapon.ConnectCamera(data.Cam);
        }
    }

    public void Interact() => DropAnItem();

    public void DropAnItem()
    {
        if (item != null)
        {
            weapon.SetReady(false);

            Vector3 force = data.CameraForward * data.DropItemStrength;
            item.Drop(dropPos, force);
            item = null;
        }
    }

    private void OnKept()
    {
        weapon.Initialize(data);
        weapon.SetReady(true);
    }

    public void OnMousePosUpdate(Vector2 screenPoint) => dropPos = thrower.GetDropPos(item, screenPoint);
}
