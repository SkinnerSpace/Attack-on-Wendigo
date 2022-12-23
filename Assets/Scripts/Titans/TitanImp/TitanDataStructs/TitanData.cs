
public class TitanData : ITitanData
{
    private readonly TitanTypes titan; public TitanTypes Type => titan;
    private readonly string name; public string Name => name;
    private readonly float speed; public float Speed => speed;
    private readonly float rotationSpeed; public float RotationSpeed => rotationSpeed;

    public TitanData(TitanSetup setup)
    {
        titan = setup.titan;
        speed = setup.speed;
        rotationSpeed = setup.rotationSpeed;
    }
}
