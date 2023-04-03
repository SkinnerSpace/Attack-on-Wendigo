using System.Collections;
using System;
using UnityEngine;
using UnityEditor;
using Character;

public class WeaponKeeper : IKeeper
{
    private ICharacterData data;
    private IInputReader input;
    public Transform Root => data.Cam.transform;

    private IPickable pickable;
    private IWeapon weapon;
    private WeaponThrower thrower;

    public WeaponKeeper(CharacterData data, IInputReader input)
    {
        this.data = data;
        this.input = input;

        thrower = new WeaponThrower(data);
    }

    public void Take(IPickable pickable, IWeapon weapon)
    {
        this.pickable = pickable;
        this.weapon = weapon;

        pickable.PickUp(this, OnCameToHands);

        AimPresenter.Instance.SetAnimation(weapon.AimAnimation, weapon.Rate);
        AimPresenter.Instance.SetCombatMode();
    }

    public void DropAnItem(Vector2 screenPoint)
    {
        if (pickable != null)
        {
            weapon.SetReady(false);
            weapon = null;

            Vector3 dropPos = thrower.GetDropPos(pickable, screenPoint);
            Vector3 force = data.CameraForward * data.DropItemStrength;

            pickable.Drop(dropPos, force);
            pickable = null;

            AimPresenter.Instance.SetIdleMode();
        }
    }

    private void OnCameToHands()
    {
        if (weapon != null)
        {
            weapon.InitializeOnTake(data, input);
            weapon.SetReady(true);
        }
    }
}
