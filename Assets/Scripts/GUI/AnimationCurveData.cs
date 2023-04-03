using UnityEngine;

[CreateAssetMenu(fileName ="AnimationCurve", menuName ="ScriptableObjects/AnimationCurve")]
public class AnimationCurveData : ScriptableObject
{
    public AnimationCurve animationCurve;
    public float animationTime;
}
