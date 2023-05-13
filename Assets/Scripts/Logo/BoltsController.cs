﻿using System;
using UnityEngine;

[ExecuteAlways]
public class BoltsController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 destination;
    [SerializeField] private float piercingTime;
    [SerializeField] private float piercedTime;
    [SerializeField] private AnimationCurve curve;

    [Header("Bolts")]
    [SerializeField] private RectTransform leftBolt;
    [SerializeField] private RectTransform rightBolt;

    private float time;
    private bool isPlaying;
    private bool isPierced;

    private Action callBackOnPierced;
    public void Play(Action callBackOnPierced)
    {
        this.callBackOnPierced = callBackOnPierced;
        Play();
    }

    public void Play()
    {
        time = 0f;
        isPlaying = true;
        isPierced = false;
    }

    public void Stop()
    {
        time = 0f;
        isPlaying = false;
        isPierced = false;

        UpdatePiercing();
    }

    private void Update()
    {
        if (isPlaying)
        {
            CountDown();
            UpdatePiercing();
        }
    }

    private void CountDown()
    {
        time += Time.unscaledDeltaTime;

        if (time >= piercingTime)
        {
            time = piercingTime;
            isPlaying = false;
        }

        if (!isPierced && time >= piercedTime)
        {
            isPierced = true;
            callBackOnPierced();
        }
    }

    private void UpdatePiercing()
    {
        float curvePosition = Mathf.InverseLerp(0f, piercingTime, time);
        float value = curve.Evaluate(curvePosition);
        Vector2 rightPosition = Vector2.LerpUnclamped(Vector2.zero, destination, value);
        Vector2 leftPosition = new Vector2(rightPosition.x * -1f, rightPosition.y);

        rightBolt.anchoredPosition = rightPosition;
        leftBolt.anchoredPosition = leftPosition;
    }
}
