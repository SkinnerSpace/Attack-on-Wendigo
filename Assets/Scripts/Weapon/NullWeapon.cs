using System.Collections.Generic;
using UnityEngine;

public class NullWeapon : IWeapon
{
    public bool IsReady => false;
    public Vector3 DefaultPosition => default;

    private static IWeapon instance;
    public static IWeapon Instance => GetSingleInstance();

    private NullWeapon() { }

    private static IWeapon GetSingleInstance(){
        if (instance == null) instance = new NullWeapon();
        return instance;
    }

    public void SetReady(bool isReady){}

    public void Aim(bool isAiming){}

    public void HoldTheTrigger(){}
    public void PressTheTrigger() {}
    public void Reload(){}

    public void InitializeOnTake(ICharacterData characterData, IInputReader inputReader)
    {
  
    }
}
