using UnityEngine;

public static class Layers
{
    public const int Props = 11;
    public const int Damageable = 12;
    public const int Player = 13;
    public const int PropDestroyers = 14;
    public const int Projectiles = 15;

    public const int Damaging = 17;
    public const int Barriers = 18;

    public static LayerMask Vision = ~(
        1 << Props | 
        1 << Player | 
        1 << PropDestroyers | 
        1 << Projectiles | 
        1 << Damaging | 
        1 << Barriers);
}

