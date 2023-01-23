using UnityEngine;

public class NullWeapon : IWeapon
{
    public bool isReady => false;
    public Vector3 DefaultPosition => default;

    private static IWeapon instance;
    public static IWeapon Instance => GetSingleInstance();

    private NullWeapon() { }

    private static IWeapon GetSingleInstance(){
        if (instance == null) instance = new NullWeapon();
        return instance;
    }

    public void GetReady(bool isReady){}

    public void Aim(bool isAiming){}

    public void PullTheTrigger(bool pull){}

    public void Reload(){}
}
