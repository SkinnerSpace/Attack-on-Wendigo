using UnityEngine;

public class Torso : IStepMagnitudeObserver
{
    public const float INTENSITY_MODIFIER = 0.2f;

    private readonly ITransformProxy transform;

    private Vector3 originalPosition;
    private Vector3 originalAngle;

    private Vector3 posDeviation;
    private Vector3 angleDeviation;

    public Torso(ITransformProxy transform)
    {
        this.transform = transform;
        originalPosition = transform.LocalPosition;
        originalAngle = transform.LocalAngle;
    } 

    public void SetPosAndAngleDeviations(Vector3 posDeviation, Vector3 angleDeviation)
    {
        this.posDeviation = posDeviation;
        this.angleDeviation = angleDeviation;
    }

    public void ReceiveStepMagnitude(float stepMagnitude)
    {
        float torsoModifier = stepMagnitude * INTENSITY_MODIFIER;

        transform.LocalPosition = originalPosition + (posDeviation * Mathf.Abs(torsoModifier));
        transform.LocalAngle = originalAngle + (angleDeviation * torsoModifier);
    }
}
