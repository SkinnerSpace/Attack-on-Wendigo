﻿using System;
using UnityEngine;

public interface IPickable
{
    void PickUp(IKeeper holder, Action callback);
    void Drop(Vector3 force);
}