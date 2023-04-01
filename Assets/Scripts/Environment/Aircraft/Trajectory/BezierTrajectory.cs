﻿using System;
using UnityEngine;

public class BezierTrajectory : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private BezierPointsManager pointsManager;
    [SerializeField] private BezierConstellator constellator;
    [SerializeField] private BezierLUT bezierLUT;
    [SerializeField] private BezierProjector projector;
    
    [Header("Settings")]
    [Range(3,128)]
    [SerializeField] private int details = 32;
    [SerializeField] public float distance;
    [SerializeField] private bool visualize;

    [SerializeField] private BezierArrangement arrangement;

    public float Length => bezierLUT.arcLength;
    public bool Visualize => visualize;

    private void OnEnable()
    {
        pointsManager.SetUpPoints(this);
    }

    private void OnValidate() => UpdatePoints();


    public Vector3 GetPosition(float inDistance, Action onFinish)
    {
        float time = bezierLUT.GetTimeFromDistance(inDistance);

        if (time >= 1f){
            time = 1f;
            onFinish();
        }

        Vector3 position = projector.GetPoint(pointsManager.Points, time);
        return position;
    }

    public void UpdatePoints()
    {
        pointsManager.SetUpPoints(this);
        pointsManager.ExtractPositionOfBezierPoints();
        bezierLUT.UpdateLUT(pointsManager.Points, details);
    }

    public void RenderBezierCurve()
    {
        if (pointsManager.isReady)
        {
            float time = bezierLUT.GetTimeFromDistance(distance);
            projector.DrawPoints(pointsManager.Points, time);
            projector.DrawCurve(pointsManager.Points, details);
        }
    }

    public void GenerateTrajectory()
    {
        constellator.RearrangePoints(pointsManager.BezierPoints, arrangement);
        pointsManager.PushThePoints();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() => RenderBezierCurve();
#endif
}