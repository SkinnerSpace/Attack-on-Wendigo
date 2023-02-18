﻿using System;
using UnityEngine;

[Serializable]
public struct VisionTarget
{
    public bool IsValid { get; set; }
    public Transform Transform { get; set; }

    public Type type;
    public float distance;
}