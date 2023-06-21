using System.Collections;
using System;
using UnityEngine;
using UnityEditor;
using Character;

public class ItemsKeeper : IItemsKeeper
{
    private ICharacterData data;
    private IInputReader input;
    private IInteractionController interactor;
    public Transform Root => data.Cam.transform;

    private IPickable pickable;
    private IItem item;
    private IWeapon weapon;
    private IHealthPack healthPack;
    private WeaponThrower thrower;

    public ItemsKeeper(CharacterData data, IInputReader input, IInteractionController interactor)
    {
        this.data = data;
        this.input = input;
        this.interactor = interactor;

        thrower = new WeaponThrower(data);
    }

    public void TakeAWeapon(IPickable pickable, IWeapon weapon)
    {
        PickAnItem(pickable, weapon);

        this.weapon = weapon;
        AimPresenter.Instance.SetAnimation(weapon.AimAnimation, weapon.Rate);
        AimPresenter.Instance.SetCombatMode();
    }

    public void TakeAHealthPack(IPickable pickable, IHealthPack healthPack, IHealthSystem healthSystem)
    {
        PickAnItem(pickable, healthPack);

        this.healthPack = healthPack;
        healthPack.SetTarget(healthSystem);
    }

    private void PickAnItem(IPickable pickable, IItem item){
        this.pickable = pickable;
        this.item = item;
        pickable.PickUp(this, OnCameToHands);
    }

    public void DropAnItem(Vector2 screenPoint)
    {
        if (pickable != null && pickable.IsReady)
        {
            ResetAnItem();

            Vector3 dropPos = thrower.GetDropPos(pickable, screenPoint);
            Vector3 force = data.CameraForward * data.DropItemStrength;

            pickable.Drop(dropPos, force);
            pickable = null;

            AimPresenter.Instance.SetIdleMode();
        }
    }

    private void ResetAnItem()
    {
        if (item != null){
            item.SetReady(false);
            item = null;
            weapon = null;
            healthPack = null;
        }
    }

    private void OnCameToHands()
    {
        if (weapon != null)
        {
            weapon.InitializeOnTake(data, input, interactor);
            weapon.SetReady(true);
            return;
        }

        if (healthPack != null)
        {
            healthPack.InitializeOnTake(data, input, interactor);
            healthPack.SetReady(true);

            return;
        }
    }
}
