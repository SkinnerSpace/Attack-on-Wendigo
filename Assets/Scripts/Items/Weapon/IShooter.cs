using System;

public interface IShooter
{
    void Shoot(bool isFiring, Action onShot);
}
