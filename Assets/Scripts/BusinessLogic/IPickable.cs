using System;
using UnityEngine;

public interface IPickable : IInteractable
{
    Transform Transform { get; }
    Vector3 Position { get; }
    void PickUp(IItemsKeeper keeper, Action callback);
    void Drop(Vector3 pos, Vector3 force);
}
