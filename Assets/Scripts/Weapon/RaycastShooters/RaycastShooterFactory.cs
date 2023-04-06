public static class RaycastShooterFactory
{
    public static RaycastShooter Create(Shooters shooter, WeaponData data, FunctionTimer timer)
    {
        switch (shooter)
        {
            case Shooters.Bullet:
                return new BulletShooter(data, timer);

            case Shooters.Buckshot:
                return new BuckshotShooter(data, timer);
        }

        return null;
    }
}
