
public class FakeTitanData : ITitanData
{
    public TitanTypes titan; public TitanTypes Type => titan;
    public string name; public string Name => name;
    public float speed; public float Speed => speed;
    public float rotationSpeed; public float RotationSpeed => rotationSpeed;

    public FakeTitanData() { }
}
