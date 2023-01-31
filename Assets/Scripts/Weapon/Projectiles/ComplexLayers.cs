using UnityEngine;

public static class ComplexLayers
{
    public static LayerMask Vision = ~(
        1 << (int)Layers.Props |
        1 << (int)Layers.Player |
        1 << (int)Layers.PropDestroyers |
        1 << (int)Layers.Projectiles |
        1 << (int)Layers.Damaging |
        1 << (int)Layers.Barriers) |
        1 << (int)Layers.RagDoll;
}
