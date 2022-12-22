
public class TitanData : ITitanData
{
    private readonly TitanTypes titan; public TitanTypes Type => titan;
    private readonly string name; public string Name => name;
    private readonly float speed; public float Speed => speed;

    public TitanData(TitanSetup setup)
    {
        speed = setup.speed;
        titan = setup.titan;
    }
}
