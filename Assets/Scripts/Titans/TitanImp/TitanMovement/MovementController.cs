using UnityEngine;

public class MovementController : IMovementController, ITorsoController
{
    public ILegsSync LegsSync { get; private set; }
    public ITorso Torso { get; private set; }
    public IUnityService UnityService { get; private set; }
    private readonly ITransformProxy transform;

    public MovementController(ITransformProxy transform)
    {
        this.transform = transform;
        UnityService = new UnityService();
    }

    public void SetUnityService(IUnityService UnityService)
    {
        this.UnityService = UnityService;
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

    public void Look(Vector3 direction, float speed)
    {
        Vector3 targetAngle = CalculateAngle(direction);
        Debug.Log(targetAngle);
        //transform.Angle = Vector3.Lerp(transform.Angle, targetAngle, speed * UnityService.Delta);
    }

    public Vector3 CalculateAngle(Vector3 direction)
    {
        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 targetAngle = new Vector3(0f, angleY, 0f);

        return targetAngle;
    }

    public void Move(float speed)
    {
        transform.Position += (transform.Forward * speed) * UnityService.Delta;
       
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

