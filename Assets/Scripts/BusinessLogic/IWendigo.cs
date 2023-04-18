using System;
using UnityEngine;

public interface IWendigo
{
    Animator Animator { get; }
    IChronos Chronos { get; }
    void SetTarget(Transform target);
    void SetHealth(int healthAmount);
    void SetSpeed(float speedMultiplier);
    void SetMaxFireballAttackDistance(float maxFireballAttackDistance);
    void SubscribeOnDeath(Action<Transform> notifyOnDeath);
}