using UnityEngine;

[CreateAssetMenu(fileName ="HighlightData", menuName ="ScriptableObjects/HighlightData")]
public class HighlightData : ScriptableObject
{
    public Vector3 deviationRange;
    public float changeSpeed;
    public float switchTime;
}