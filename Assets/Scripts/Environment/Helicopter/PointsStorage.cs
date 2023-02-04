using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PointsStorage : MonoBehaviour
{
    public Vector3[] positions;

    private void OnEnable() => UpdatePoints();

    public void UpdatePoints() => positions = ExtractPointsArray();

    private Vector3[] ExtractPointsArray()
    {
        Transform[] points = GetComponentsInChildren<Transform>();
        Vector3[] positions = new Vector3[points.Length-1];

        for (int i = 1; i < points.Length; i++){
            positions[i-1] = points[i].position;
        }
            
        return positions;
    }
}
