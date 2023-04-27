using System.Collections.Generic;
using UnityEngine;

public interface IWeapon : IItem
{
    AimAnimationsPack AimAnimation { get; }
    float Rate { get; }

    void Aim(bool isAiming);
    void HoldTheTrigger();
    void PressTheTrigger();
    void Reload();
}
