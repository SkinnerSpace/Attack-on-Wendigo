﻿using System;
using UnityEngine;

public class BezierLUT : MonoBehaviour
{
    [SerializeField] private BezierProjector projector;

    private float[] LUT;
    public float arcLength { get; private set; }
    public int segmentsCount { get; private set; }

    public void UpdateLUT(Vector3[] points, int details)
    {
        LUT = GenerateLUT(points, details);
        arcLength = LUT[LUT.Length - 1];
        segmentsCount = LUT.Length;
    }

    public float[] GenerateLUT(Vector3[] points, int details)
    {
        float[] lookUpTable = new float[details];
        lookUpTable[0] = 0f;

        Vector3 previousPoint = points[0];
        for (int i = 1; i < details; i++)
        {
            float tDraw = i / (details - 1f);
            Vector3 point = projector.GetPoint(points, tDraw);

            lookUpTable[i] = (i > 1) ? (lookUpTable[i - 1] + Vector3.Distance(previousPoint, point)) : (Vector3.Distance(previousPoint, point));

            previousPoint = point;
        }

        return lookUpTable;
    }

    public float GetTimeFromDistance(float distance)
    {
        if (distance.Between(0, arcLength)){
            for (int i=0; i< segmentsCount; i++){
                if (distance.Between(LUT[i], LUT[i + 1])){
                    return distance.Remap(
                        LUT[i], 
                        LUT[i + 1], 
                        i / (segmentsCount - 1f), 
                        (i + 1) / (segmentsCount - 1f)
                        );
                }
            }
        }

        return distance / arcLength;
    }
}
