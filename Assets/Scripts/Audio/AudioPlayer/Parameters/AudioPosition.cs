using System;
using UnityEngine;

public class AudioPosition : AudioParam
{
    public new static Type parameter => typeof(AudioPosition);

    private Vector3 position;

    public override void Set(Vector3 position) => this.position = position;

    public override void ApplyTo(AudioEvent audioEvent) => audioEvent.Set3DPosition(position);

    public override string ToString() => $"Position: {position}" ;
}
