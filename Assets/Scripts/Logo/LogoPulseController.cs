using System;
using UnityEditor;
using UnityEngine;

public class LogoPulseController : MonoBehaviour
{
    private const float MAX_SIN_TIME = Mathf.PI * 2f;

    [Header("Next stage")]
    [SerializeField] private LogoFaceController faceController;
    [SerializeField] private RainbowController rainbowController;

    [Header("Required components")]
    [SerializeField] private RectTransform[] scaledElements;
    [SerializeField] private CanvasGroup[] shockedElements;

    [Header("Settings")]
    [SerializeField] private AnimationCurve intensityCurve;
    [SerializeField] private float pulseTime;
    [SerializeField] private float maxScale;
    [SerializeField] private float frequency;

    private static (float start, float current) time;
    private static (float start, float current) sinTime;

    private static float sin;
    private static float pulseValue;
    private static float pulseIntensity;
    private static bool isShocked;

    public void Play()
    {
        time.current = 0f;
        time.start = Time.realtimeSinceStartup;

        sin = Mathf.PI * 2f;
        isShocked = true;
    }
    
    public void Stop()
    {
        time.current = 0f;
        sinTime.current = 0f;

        sin = -1f;
        isShocked = false;

        UpdatePulse();
    }

    private void Update()
    {
        if (isShocked)
        {
            CountDown();
            Oscillate();
            UpdatePulse();
        }
    }

    private void CountDown()
    {
        time.current = Time.realtimeSinceStartup - time.start;

        if (time.current >= pulseTime){
            time.current = pulseTime;
            isShocked = false;

            OnShocked();
        }
    }

    private void OnShocked()
    {
        faceController.SetStage(LogoAnimationStages.Final);
        rainbowController.Play();
    }

    private void Oscillate()
    {
        sinTime.current = (Time.realtimeSinceStartup - sinTime.start) * frequency;

        if (sinTime.current > MAX_SIN_TIME)
        {
            sinTime.current -= MAX_SIN_TIME;
        }

        sin = Mathf.Sin(sinTime.current);
    }

    private void UpdatePulse()
    {
        float curvePosition = Mathf.InverseLerp(0f, pulseTime, time.current);
        pulseIntensity = intensityCurve.Evaluate(curvePosition);

        pulseValue = Mathf.InverseLerp(-1f, 1f, sin);
        pulseValue *= pulseIntensity;
        pulseValue = Easing.QuadEaseInOut(pulseValue);

        UpdateColor();
        UpdateScale();
    }

    private void UpdateColor()
    {
        foreach (CanvasGroup shockedElement in shockedElements)
        {
            shockedElement.alpha = pulseValue;
        }
    }

    private void UpdateScale()
    {
        float dimensionValue = Mathf.Lerp(1f, maxScale, pulseValue);
        Vector3 scale = new Vector3(dimensionValue, dimensionValue, 1f);

        foreach (RectTransform scaledElement in scaledElements)
        {
            scaledElement.localScale = scale;
        }
    }
}
