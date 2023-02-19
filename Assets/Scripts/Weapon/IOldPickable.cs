using System;
using UnityEngine;

public interface IOldPickable
{
    void PickUp(IHolder holder, Action callback);
    void Drop(Vector3 force);
}
