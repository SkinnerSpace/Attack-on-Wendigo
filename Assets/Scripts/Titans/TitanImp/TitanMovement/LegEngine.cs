using UnityEngine;

public class LegEngine : ILegEngine
{
    private const float SPEED_ADJUSTMENT = 0.2f;

    public float StepHeight { get; private set; }
    public float Speed { get; private set; }
    public float Lerp { get; set; }

    private readonly ILeg leg;
    public IUnityService UnityService { get; private set; }

    public LegEngine(ILeg leg, float Speed, float StepHeight)
    {
        this.leg = leg;
        this.Speed = Speed * SPEED_ADJUSTMENT;
        this.StepHeight = StepHeight;

        UnityService = new UnityService();
    }

    public void SetUnityService(IUnityService UnityService)
    {
        this.UnityService = UnityService;
    }

    public Vector3 UpdatePosition(Vector3 oldPos, Vector3 newPos)
    {
        Vector3 currentPos;

        IncrementLerp(Speed);
        currentPos = CalculatePosition(oldPos, newPos);
        ResetLerpIfMax();

        return currentPos;
    }

    public void IncrementLerp(float Speed)
    {
        Lerp += Speed * UnityService.Delta;
        if (Lerp >= 1f)
        {
            Lerp = 1f;
            leg.StepIsOver();
        }
    }

    public Vector3 CalculatePosition(Vector3 oldPos, Vector3 newPos)
    {
        Vector3 footPos = Vector3.Lerp(oldPos, newPos, Lerp);
        footPos.y += (Mathf.Sin(Lerp * Mathf.PI) * StepHeight);

        return footPos;
    }

    public void ResetLerpIfMax()
    {
        if (Lerp >= 1f)
            Lerp = 0f;
    }
}