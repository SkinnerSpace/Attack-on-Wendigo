using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    Vector3 DefaultPosition { get; }

    void SetReady(bool isReady);
    void Aim(bool isAiming);
    void HoldTheTrigger();
    void PressTheTrigger();
    void Reload();
}
