using UnityEngine;

[ExecuteAlways]
public class BezierLUT : MonoBehaviour
{
    [SerializeField] private PointsStorage storage;
    [SerializeField] private int details;

    private float[] LUT;

    private void OnEnable() => UpdateLUT();

    public void UpdateLUT() => LUT = GenerateLUT(storage.Points, details);

    public float[] GenerateLUT(Vector3[] points, int details)
    {
        float[] lookUpTable = new float[details];
        lookUpTable[0] = 0f;

        Vector3 prev = points[0];
        for (int i = 1; i < details; i++)
        {
            float tDraw = i / (details - 1f);
            Vector3 point = BezierPointProjector.GetPoint(points, tDraw);

            lookUpTable[i] = (i > 1) ? (lookUpTable[i - 1] + Vector3.Distance(prev, point)) : (Vector3.Distance(prev, point));

            prev = point;
        }

        return lookUpTable;
    }

    public float GetTimeFromDistance(float distance)
    {
        float arcLength = LUT[LUT.Length - 1];

        if (distance.Between(0, arcLength)){
            return IterateThroughSegments(distance);
        }

        return distance / arcLength;
    }

    private float IterateThroughSegments(float distance)
    {
        int segmentsCount = LUT.Length;

        for (int i = 0; i < segmentsCount - 1; i++)
        {
            if (distance.Between(LUT[i], LUT[i + 1]))
            {
                return distance.Remap(
                    LUT[i],
                    LUT[i + 1],
                    i / (segmentsCount - 1f),
                    (i + 1) / (segmentsCount - 1f)
                );
            }
        }
        return 0f;
    }
}

