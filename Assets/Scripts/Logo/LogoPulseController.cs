using UnityEngine;

[ExecuteInEditMode]
public class LogoPulseController : MonoBehaviour
{
    private const float MAX_SIN_TIME = Mathf.PI * 2f;

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
    private float sin;
    private float pulseValue;
    private float pulseIntensity;
    private bool isShocked;

    public float openTime;

    public void Play()
    {
        time = 0f;
        sinTime = Mathf.PI * 2f;
        isShocked = true;
    }
    
    public void Stop()
    {
        time = 0f;
        sinTime = 0f;
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

        openTime = time;
        Debug.Log("SHOCk");
    }

    private void CountDown()
    {
        time += Time.unscaledDeltaTime;

        if (time >= pulseTime){
            time = pulseTime;
            isShocked = false;
        }
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
