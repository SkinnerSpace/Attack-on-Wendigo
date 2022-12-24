using UnityEngine;

public abstract class TitanData
{
    public TitanTypes type;
    public string name;
    public float speed;
    public float rotationSpeed;

    public float stepDistance;
    public float stepSpacing;
    public float stepHeight;

    public Vector3 torsoPosDeviation;
    public Vector3 torsoAngleDeviation;

    public TitanData() { }

    public TitanData(TitanSetup setup)
    {
        type = setup.titanType;
        name = setup.titanName;
        speed = setup.speed;
        rotationSpeed = setup.rotationSpeed;

        stepDistance = setup.stepDistance;
        stepSpacing = setup.stepSpacing;
        stepHeight = setup.stepHeight;

        torsoPosDeviation = setup.torsoPosDeviation;
        torsoAngleDeviation = setup.torsoAngleDeviation;
    }
}
