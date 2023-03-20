public interface ISurface
{
    ISurfaceHitBuilder Hit();
    ISurfaceHitBuilder Hit(string particleName);
}
