using UnityEngine;

public enum Layers
{
    Default = 0,
    Weapon = 10,
    Props = 11,
    Damageable = 12,
    Player = 13,
    PropDestroyers = 14,
    Projectiles = 15,
    Damaging = 17,
    Barriers = 18,
}

public static class ComplexLayers
{
    public static LayerMask Vision = ~(
        1 << (int)Layers.Props |
        1 << (int)Layers.Player |
        1 << (int)Layers.PropDestroyers |
        1 << (int)Layers.Projectiles |
        1 << (int)Layers.Damaging |
        1 << (int)Layers.Barriers);
}
