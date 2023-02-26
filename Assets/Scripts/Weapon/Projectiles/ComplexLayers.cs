using UnityEngine;

public static class ComplexLayers
{
    public static LayerMask VisionExclusion = ~(
        1 << (int)Layers.Weapon |
        1 << (int)Layers.Props |
        1 << (int)Layers.Player |
        1 << (int)Layers.PropDestroyers |
        1 << (int)Layers.Projectiles |
        1 << (int)Layers.Damaging |
        1 << (int)Layers.Barriers |
        1 << (int)Layers.RagDoll
        );

    public static LayerMask Interactables =
        1 << (int)Layers.Interactables;

    public static LayerMask Landscape =
        1 << (int)Layers.Landscape;

    public static LayerMask Combat = ~(
        1 << (int)Layers.Props |
        1 << (int)Layers.Player |
        1 << (int)Layers.PropDestroyers |
        1 << (int)Layers.Projectiles |
        1 << (int)Layers.Damaging |
        1 << (int)Layers.Barriers |
        1 << (int)Layers.RagDoll |
        1 << (int)Layers.Items
        );

    public static LayerMask Solid =
        1 << (int)Layers.Ground |
        1 << (int)Layers.Landscape;
}
