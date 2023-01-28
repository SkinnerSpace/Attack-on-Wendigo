using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    bool isReady { get; }
    Vector3 DefaultPosition { get; }

    void GetReady(bool isReady);
    void Aim(bool isAiming);
    void PullTheTrigger(bool pull);
    void Reload();
}
