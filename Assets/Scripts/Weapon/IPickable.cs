using System;
using UnityEngine;

public interface IPickable
{
    void PickUp(IHolder holder, Action callback);
    void Drop(Vector3 force);
}
