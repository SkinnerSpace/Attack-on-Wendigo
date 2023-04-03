using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    Vector3 DefaultPosition { get; }
    AimAnimationsPack AimAnimation { get; }
    float Rate { get; }

    void SetReady(bool isReady);
    void Aim(bool isAiming);
    void HoldTheTrigger();
    void PressTheTrigger();
    void Reload();
    void InitializeOnTake(ICharacterData characterData, IInputReader inputReader);
}
