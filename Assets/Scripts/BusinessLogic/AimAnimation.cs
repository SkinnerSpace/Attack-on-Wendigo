using UnityEngine;

[CreateAssetMenu(fileName ="AimAnimationCurve", menuName ="ScriptableObjects/AimAnimationCurve")]
public class AimAnimation : ScriptableObject
{
    public AnimationCurve distance;
    public AnimationCurve rotation;
    public AnimationCurve scale;
}
