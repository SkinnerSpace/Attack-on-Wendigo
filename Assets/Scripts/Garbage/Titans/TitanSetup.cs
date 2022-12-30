
using UnityEngine;

public abstract class TitanSetup : ScriptableObject
{
    public string titanName;
    public TitanTypes titanType;
    public float speed;
    public float rotationSpeed;

    public float stepDistance;
    public float stepSpacing;
    public float stepHeight;

    public Vector3 torsoPosDeviation;
    public Vector3 torsoAngleDeviation;
}
