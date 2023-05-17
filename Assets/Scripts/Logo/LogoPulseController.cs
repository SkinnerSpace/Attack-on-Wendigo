using System;
using UnityEditor;
using UnityEngine;

public class LogoPulseController : MonoBehaviour
{
    private const float MAX_SIN_TIME = Mathf.PI * 2f;

    [Header("Next stage")]
    [SerializeField] private LogoFaceController faceController;
    [SerializeField] private RainbowController rainbowController;
    [SerializeField] private TitleAnimator titleAnimator;
    [SerializeField] private UpscaleAnimator upscaleAnimator;

    [Header("Required components")]
    [SerializeField] private RectTransform[] scaledElements;
    [SerializeField] private CanvasGroup[] shockedElements;

    [Header("Settings")]
    [SerializeField] private AnimationCurve intensityCurve;
    [SerializeField] private float pulseTime;
    [SerializeField] private float maxScale;
    [SerializeField] private float frequency;

    private float time;
    private float sinTime;

    private static float sin;
    private static float pulseValue;
    private static float pulseIntensity;
    private static bool isShocked;

    public void Play()
    {
        time = 0f;

        sin = Mathf.PI * 2f;
        isShocked = true;
    }
    
    public void Stop()
    {
        time = 0f;

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
        time += Time.unscaledDeltaTime;

        if (time >= pulseTime){
            time = pulseTime;
            isShocked = false;

            OnShocked();
        }
    }

    private void OnShocked()
    {
        faceController.SetStageAndPushSecondaryElements(LogoAnimationStages.Final);
        rainbowController.Play();
        titleAnimator.Appear();
        upscaleAnimator.Increase();
    }

    private void Oscillate()
    {
        sinTime += Time.unscaledDeltaTime * frequency;

        if (sinTime > MAX_SIN_TIME)
        {
            sinTime -= MAX_SIN_TIME;
        }

        sin = Mathf.Sin(sinTime);
    }

    private void UpdatePulse()
    {
        float curvePosition = Mathf.InverseLerp(0f, pulseTime, time);
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
