using UnityEngine;

public class Torso : ITorso
{
    private const float INTENSITY_MODIFIER = 0.2f;

    private readonly ITransformProxy transform;
    private ITorsoController torsoController;

    private Vector3 originalPosition;
    private Vector3 originalAngle;

    private Vector3 posDeviation;
    private Vector3 angleDeviation;

    public Torso(TitanSetup titanSetup, ITransformProxy transform)
    {
        posDeviation = titanSetup.torsoPosDeviation;
        angleDeviation = titanSetup.torsoAngleDeviation;

        this.transform = transform;
        originalPosition = transform.LocalPosition;
        originalAngle = transform.LocalAngle;
    }

    public void SetTorsoController(ITorsoController torsoController)
    {
        this.torsoController = torsoController;
    }

    public void Update()
    {
        float torsoModifier = torsoController.GetTorsoModifier() * INTENSITY_MODIFIER;

        transform.LocalPosition = originalPosition + (posDeviation * Mathf.Abs(torsoModifier));
        transform.LocalAngle = originalAngle + (angleDeviation * torsoModifier);
    }
}
