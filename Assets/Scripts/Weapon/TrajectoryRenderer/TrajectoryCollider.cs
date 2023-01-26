using System.Collections.Generic;
using UnityEngine;

public class TrajectoryCollider
{
    private LayerMask detectionLayers = 1 << 8 | 1 << 12;
    public EndPoint endPoint { get; private set; }
    public bool hasDetected { get; private set; }

    public TrajectoryCollider() => endPoint = new EndPoint();

    public void TakeAProbe(List<Vector3> points, int i)
    {
        hasDetected = false;

        if (i >= 1)
        {
            Vector3 position = points[i];
            Vector3 lastPosition = points[i - 1];
            Vector3 vector = position - lastPosition;
            Vector3 rayDir = vector.normalized;

            Ray ray = new Ray(lastPosition, rayDir);
            CastTheEndPoint(ray, vector.magnitude);
        }
    }

    private void CastTheEndPoint(Ray ray, float distance)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, distance, detectionLayers))
        {
            endPoint.position = hit.point + (hit.normal * 0.1f);
            endPoint.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(-90, 0, 0);

            hasDetected = true;
        }
    }
}
