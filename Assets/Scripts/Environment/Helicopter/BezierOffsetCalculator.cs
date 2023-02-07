using UnityEngine;

public class BezierOffsetCalculator : MonoBehaviour
{
    public Vector3 GetStraight(BezierArrangement arrangement, Vector3 direction, float sign) => (direction * sign) * GetStraightOffset(arrangement);
    public Vector3 GetPerpendicular(BezierArrangement arrangement, Vector3 direction) => direction * GetPerpendicularOffset(arrangement) * GetRandSign();

    private float GetStraightOffset(BezierArrangement arrangement) => Rand.Range(arrangement.straightMinOffset, arrangement.straightMaxOffset);
    private float GetPerpendicularOffset(BezierArrangement arrangement) => Rand.Range(arrangement.perpendicularMinOffset, arrangement.perpendicularMaxOffset);
    private float GetRandSign() => Rand.Range(0f, 1f) < 0.5f ? -1f : 1f;
}