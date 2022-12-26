public class Mover
{
    public IClock clock { get; private set; }


    private readonly ITransformProxy transform;
    private float speed;

    public Mover(ITransformProxy transform, IClock clock, float speed)
    {
        this.transform = transform;
        this.clock = clock;
        this.speed = speed;
    }

    public void MoveForward()
    {
        transform.Position += (transform.Forward * speed) * clock.Delta;
    }
}

