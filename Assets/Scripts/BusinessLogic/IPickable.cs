using System;
using UnityEngine;

public interface IPickable
{
    Transform Transform { get; }
    Vector3 Position { get; }
    void PickUp(IKeeper keeper, Action callback);
    void Drop(Vector3 pos, Vector3 force);
}