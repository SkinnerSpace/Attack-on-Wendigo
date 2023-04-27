using System.Collections;
using System;
using UnityEngine;
using UnityEditor;
using Character;

public class ItemsKeeper : IItemsKeeper
{
    private ICharacterData data;
    private IInputReader input;
    public Transform Root => data.Cam.transform;

    private IPickable pickable;
    private IWeapon weapon;
    private IHealthPack healthPack;
    private WeaponThrower thrower;

    public ItemsKeeper(CharacterData data, IInputReader input)
    {
        this.data = data;
        this.input = input;

        thrower = new WeaponThrower(data);
    }

    public void TakeAWeapon(IPickable pickable, IWeapon weapon)
    {
        PickAnItem(pickable);

        this.weapon = weapon;
        AimPresenter.Instance.SetAnimation(weapon.AimAnimation, weapon.Rate);
        AimPresenter.Instance.SetCombatMode();
    }

    public void TakeAHealthPack(IPickable pickable, IHealthPack healthPack)
    {
        PickAnItem(pickable);

        this.healthPack = healthPack;
    }

    private void PickAnItem(IPickable pickable){
        this.pickable = pickable;
        pickable.PickUp(this, OnCameToHands);
    }

    public void DropAnItem(Vector2 screenPoint)
    {
        if (pickable != null)
        {
            DropAWeapon();

            Vector3 dropPos = thrower.GetDropPos(pickable, screenPoint);
            Vector3 force = data.CameraForward * data.DropItemStrength;

            pickable.Drop(dropPos, force);
            pickable = null;

            AimPresenter.Instance.SetIdleMode();
        }
    }

    private void DropAWeapon()
    {
        if (weapon != null){
            weapon.SetReady(false);
            weapon = null;
        }
    }

    private void OnCameToHands()
    {
        if (weapon != null)
        {
            weapon.InitializeOnTake(data, input);
            weapon.SetReady(true);
            return;
        }

        if (healthPack != null)
        {
            healthPack.InitializeOnTake(data, input);
            healthPack.SetReady(true);
            return;
        }
    }
}
