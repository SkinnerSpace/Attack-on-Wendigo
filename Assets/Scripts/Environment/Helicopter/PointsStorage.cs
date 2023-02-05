using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PointsStorage : MonoBehaviour
{
    public Vector3[] Points => points;
    private Vector3[] points;

    private void OnEnable() => UpdatePoints();

    public bool IsReady => points.Length >= 2; 

    public void UpdatePoints()
    {
        points = ExtractPointsArray();
    }

    private Vector3[] ExtractPointsArray()
    {
        Transform[] entities = GetComponentsInChildren<Transform>();
        Vector3[] points = new Vector3[entities.Length-1];

        for (int i = 1; i < entities.Length; i++){
            points[i-1] = entities[i].position;
        }
            
        return points;
    }
}
