using System;
using UnityEngine;

public class AudioAnchor : AudioParam
{
    public new static Type parameter => typeof(AudioAnchor);

    private Transform anchor;

    public override void Set(Transform anchor) => this.anchor = anchor;

    public override void ApplyTo(AudioEvent audioEvent) => audioEvent.SetAnchor(anchor);

    public override string ToString() => $"Anchor: {anchor.name}";
}
