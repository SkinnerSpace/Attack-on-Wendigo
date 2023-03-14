using System;
using UnityEngine;

public abstract class AudioParam
{
    public static Type parameter => typeof(AudioParam);
    public abstract void ApplyTo(AudioEvent audioEvent);
    public virtual void Set(int value) { }
    public virtual void Set(float value) { }
    public virtual void Set(float first, float second) { }
    public virtual void Set(Vector3 position) { }
    public virtual void Set(Transform anchor) { }
}

