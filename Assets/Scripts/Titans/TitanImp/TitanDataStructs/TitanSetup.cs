
using UnityEngine;

[CreateAssetMenu(fileName = "TitanData", menuName = "ScriptableObjects/TitanSetup", order = 1)]
public class TitanSetup : ScriptableObject
{
    public string titanName;
    public TitanTypes titan;
    public float speed;

    public float stepDistance;
    public float spacing;
    public float stepHeight;

    public Vector3 torsoPosDeviation;
    public Vector3 torsoAngleDeviation;
}
