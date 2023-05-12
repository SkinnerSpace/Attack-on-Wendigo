using UnityEngine;

[CreateAssetMenu(fileName ="RainbowCurve", menuName ="ScriptableObjects/RainbowCuve")]
public class RainbowCurve : ScriptableObject
{
    [SerializeField] private AnimationCurve curve;

    public float Evaluate(float time) => curve.Evaluate(time);
}
