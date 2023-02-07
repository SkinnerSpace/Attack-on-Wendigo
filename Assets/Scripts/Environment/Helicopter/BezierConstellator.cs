using UnityEngine;

public class BezierConstellator : MonoBehaviour
{
    public void RearrangePoints(BezierPoint[] bezierPoints, BezierArrangement arrangementData)
    {
        ShiftStartToPreviousEnd(bezierPoints);
        SetRandomPos(bezierPoints, arrangementData);
    }

    private void ShiftStartToPreviousEnd(BezierPoint[] bezierPoints)
    {
        Vector3 startPos = bezierPoints[bezierPoints.Length - 1].Position;
        bezierPoints[0].SetPosition(startPos);
    }

    private void SetRandomPos(BezierPoint[] bezierPoints, BezierArrangement arrangementData)
    {
        for (int i = 1; i < bezierPoints.Length; i++)
        {
            Vector3 randomPos = GetRandPos(arrangementData);
            bezierPoints[i].SetPosition(randomPos);
        }
    }

    private Vector3 GetRandPos(BezierArrangement arrangementData)
    {
        float offsetRange = GetOffsetRange(arrangementData.minOffset, arrangementData.maxOffset);
        float height = GetHeight(arrangementData.minHeight, arrangementData.maxHeight);
        Vector3 displacement = GetDisplacement(offsetRange, height);
        Vector3 position = arrangementData.Pivot + displacement;

        return position;
    }

    private float GetOffsetRange(float minOffset, float maxOffset)
    {
        float offset = Rand.Range(-maxOffset, maxOffset);
        offset = CorrectOffsetRange(offset, minOffset);

        return offset;
    }

    private float CorrectOffsetRange(float offset, float minOffset) => Mathf.Abs(offset) < minOffset ? (Mathf.Sign(offset) * minOffset) : offset;

    private float GetHeight(float minHeight, float maxHeight) => Rand.Range(minHeight, maxHeight);

    private Vector3 GetDisplacement(float offsetRange, float height)
    {
        Vector3 displacement = new Vector3(
                x: Rand.Range(-offsetRange, offsetRange),
                y: height,
                z: Rand.Range(-offsetRange, offsetRange)
                );

        return displacement;
    }
}

