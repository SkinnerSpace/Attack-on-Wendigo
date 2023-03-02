public class FireballMover
{
    private IFireballData data;
    private IChronos chronos;

    public FireballMover(IFireballData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Move()
    {
        data.Position += data.Forward * data.Speed * chronos.DeltaTime;
    }
}
