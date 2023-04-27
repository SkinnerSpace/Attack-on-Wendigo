using System;
using UnityEngine;

public interface IHandyItem
{
    Vector3 DefaultPosition { get; }
    void SubscribeOnReady(Action onReady);
}
