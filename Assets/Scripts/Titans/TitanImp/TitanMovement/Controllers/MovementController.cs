public class MovementController : IMovementController, ITorsoController
{
    public ILegsSync LegsSync { get; private set; }
    public ITorso Torso { get; private set; }
    public IClock clock { get; private set; }


    private readonly ITransformProxy transform;

    public MovementController(ITransformProxy transform, IClock clock)
    {
        this.transform = transform;
        this.clock = clock;
    }

    public void AddLeg(ILeg leg)
    {
        if (LegsSync == null)
            LegsSync = new LegsSync();

        LegsSync.AddLeg(leg);
    }

    public void AddTorso(ITorso Torso)
    {
        this.Torso = Torso;
        Torso.SetTorsoController(this);
    }

    public void Move(float speed)
    {
        transform.Position += (transform.Forward * speed) * clock.Delta;
       
        if (LegsSync != null) LegsSync.Update();
        if (Torso != null) Torso.Update();
    }

    public float GetTorsoModifier()
    {
        if (LegsSync != null)
        {
            ILeg currentLeg = LegsSync.CurrentLeg;
            return currentLeg.Side * currentLeg.Transform.Position.y;
        }

        return 0f;
    }
}

