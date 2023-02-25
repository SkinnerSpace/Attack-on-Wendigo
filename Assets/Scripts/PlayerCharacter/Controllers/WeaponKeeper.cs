using System.Collections;
using System;
using UnityEngine;
using UnityEditor;

public class WeaponKeeper : BaseController, IKeeper, IInteractor, IMousePosObserver
{
    private ICharacterData data;
    private IInputReader input;
    public Transform Root => data.Cam.transform;

    private IPickable pickableItem;
    private Weapon weapon;
    private WeaponThrower thrower;

    private Vector3 dropPos;

    public override void Initialize(MainController main)
    {
        data = main.Data;
        thrower = new WeaponThrower(data);
        input = main.InputReader;
    }

    public override void Connect()
    {
        input.Get<InteractionInputReader>().Subscribe(this);
        input.Get<MousePositionInputReader>().Subscribe(this);
    }

    public void TakeAnItem(Transform item)
    {
        pickableItem = item.GetComponent<IPickable>();

        if (pickableItem != null)
        {
            weapon = item.parent.GetComponent<Weapon>();
            weapon.ConnectCamera(data.Cam);
            pickableItem.PickUp(this, OnKept);
        }
    }

    public void Interact() => DropAnItem();

    public void DropAnItem()
    {
        if (pickableItem != null)
        {
            weapon.SetReady(false);

            Vector3 force = data.CameraForward * data.DropItemStrength;
            pickableItem.Drop(dropPos, force);
            pickableItem = null;
        }
    }

    private void OnKept()
    {
        weapon.Initialize(data, input);
        weapon.SetReady(true);
    }

    public void OnMousePosUpdate(Vector2 screenPoint) => dropPos = thrower.GetDropPos(pickableItem, screenPoint);
}
