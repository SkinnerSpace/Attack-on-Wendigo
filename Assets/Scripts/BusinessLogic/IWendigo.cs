using System;
using UnityEngine;

public interface IWendigo
{
    Animator Animator { get; }
    IChronos Chronos { get; }
    void SetTarget(Transform target);
    void SetUp(WendigoSettings settings);
    void SubscribeOnDeath(Action<Transform> notifyOnDeath);
}
