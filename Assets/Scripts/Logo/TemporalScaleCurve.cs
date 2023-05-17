using UnityEngine;

[CreateAssetMenu(fileName ="TemporalScaleCurve", menuName ="ScriptableObjects/TemporalScaleCurve")]
public class TemporalScaleCurve : ScriptableObject
{
    public float upscaleTime;
    public AnimationCurve upscaleCurve;
}
