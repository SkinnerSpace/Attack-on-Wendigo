using System;
using UnityEngine;

public interface IPickable
{
    Vector3 Position { get; }
    void PickUp(IKeeper holder, Action callback);
    void Drop(Vector3 pos, Vector3 force);
    T Get<T>() where T : Component;
}