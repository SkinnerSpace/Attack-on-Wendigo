using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    bool isReady { get; }
    Vector3 DefaultPosition { get; }

    void SetReady(bool isReady);
    void Aim(bool isAiming);
    void PullTheTrigger();
    void Reload();
}
