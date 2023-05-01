using System;
using System.Collections.Generic;
using UnityEngine;

public class RetractionController : MonoBehaviour
{
    private enum States{
        None,
        Extension,
        Retraction
    }

    [SerializeField]
    private List<RetractionPoint> retractables;

    [Header("Time")]
    [SerializeField] private float extensionTime;
    [SerializeField] private float retractionTime;

    private States state;
    private float time;
    private float lerp;

    private Action onRetracted;

    public void Extend()
    {
        state = States.Extension;
        time = 0f;
    }

    public void Retract(Action onRetracted)
    {
        this.onRetracted = onRetracted;

        state = States.Retraction;
        time = 0f;
    }

    private void Update()
    {
        switch (state)
        {
            case States.Extension:
                ExtendOverTime();
                break;

            case States.Retraction:
                RetractOverTime();
                break;
        }
    }

    private void ExtendOverTime(){
        CountDownTill(extensionTime);

        foreach (RetractionPoint point in retractables){
            InterpolateOverTime(point.transform, point.retractedPosition, point.extendedPosition);
        }
        
        if (TimeIsOut(extensionTime)){
            state = States.None;
        }
    }

    private void RetractOverTime(){
        CountDownTill(retractionTime);

        foreach (RetractionPoint point in retractables){
            InterpolateOverTime(point.transform, point.extendedPosition, point.retractedPosition);
        }

        if (TimeIsOut(retractionTime)){
            state = States.None;
            onRetracted(); 
        }
    }

    private void CountDownTill(float targetTime)
    {
        time += Time.deltaTime;
        lerp = Mathf.InverseLerp(0f, targetTime, time);
    }

    private void InterpolateOverTime(Transform retractable, Vector3 a, Vector3 b) => retractable.localPosition = Vector3.Lerp(a, b, lerp);

    private bool TimeIsOut(float targetTime) => time >= targetTime;
 }
