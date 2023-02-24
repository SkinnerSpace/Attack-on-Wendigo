using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadPosGenerator : MonoBehaviour
{
    private const float MAX_DISTANCE = 140;
    private const float MAX_RADIUS_OFFSET = 60f;

    [SerializeField] private RadPosCalc radPosCalc;
    [SerializeField] private Transform viewer;

    [SerializeField] private float criticalDistance = 80f;
    [SerializeField] private float minRadius = 100f;
    [SerializeField] private float maxRadius = 360f;

    [SerializeField] private float horizonLength = 0.6f;

    [Header("Testing")]
    [SerializeField] private bool test;

    [Range(0f, 1f)]
    [SerializeField] private float testCloseness;

    public Vector3 CalculatePos(List<Transform> wendigos)
    {
        float closeness = (wendigos.Count > 0) ? GetCloseness(wendigos) : 0f;
        float radius = Mathf.Lerp(minRadius, maxRadius, closeness);
        Vector3 position = GetPosition(radius);

        return position;
    }

    private float GetCloseness(List<Transform> wendigos)
    {
        var closest = wendigos.OrderBy(t => (viewer.position - t.position).sqrMagnitude).First().transform;
        float distance = Vector3.Distance(viewer.position, closest.position);
        float closeness = distance / maxRadius;

        return closeness; 
    }

    private Vector3 GetPosition(float radius)
    {
        float randPosInSight = UnityEngine.Random.Range(-horizonLength, horizonLength);
        float randRadius = radius - (UnityEngine.Random.Range(0, MAX_RADIUS_OFFSET));

        Vector3 localPos = new Vector3(randPosInSight, 0f, 1f).normalized * radius;
        Vector3 rawForwardPos = radPosCalc.LocalToWorldForward(localPos, viewer);

        return radPosCalc.GetForwardPos(rawForwardPos, randRadius);
    }

    private void OnDrawGizmos()
    {
        if (test && viewer != null)
        {
            float radius = Mathf.Lerp(minRadius, maxRadius, testCloseness);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GetPosition(radius), 3f);
        }
    }
}
