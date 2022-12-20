using UnityEngine;

public class Leg : ILeg
{
    private const float SPEED_ADJUSTMENT = 0.2f;
    private Sides side; public float Side => (side == Sides.Left) ? (-1f) : (1f);

    public ITransformProxy transform { get; private set; }
    private StepPos stepPos;
    private float speed;

    public ILegsSync synchronizer { get; private set; }
    private readonly LegRaycaster raycaster;
    private readonly LegEngine engine;

    public Leg(LegSetupPack setupPack)
    {
        Unpack(setupPack);

        raycaster = new LegRaycaster(this, setupPack);
        engine = new LegEngine(this, setupPack);
    }

    private void Unpack(LegSetupPack setupPack)
    {
        side = setupPack.side;
        speed = setupPack.speed * SPEED_ADJUSTMENT;
        transform = setupPack.transform;
    }

    public void SetSynchronizer(ILegsSync synchronizer)
    {
        this.synchronizer = synchronizer;
    }

    public void MakeAStep()
    {
        stepPos.old = transform.Position;
        stepPos.novel = raycaster.GetNextStepPos();
        engine.SetActive(true);
    }

    public void Update()
    {
        stepPos.current = engine.UpdatePosition(stepPos.old, stepPos.novel, speed);
        transform.Position = stepPos.current;
    }

    public void StepIsOver()
    {
        stepPos.old = transform.Position;
        synchronizer.StepIsOver();
    }

    public void Stop()
    {
        engine.SetActive(false);
    }
}
