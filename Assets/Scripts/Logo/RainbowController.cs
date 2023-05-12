using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class RainbowController : MonoBehaviour
{
    private static class DEFAULT_OFFSET
    {
        public static float MIN = 350f;
        public static float MAX = -150f;
    }

    [SerializeField] private float valueMultiplier = 64f;
    [SerializeField] private float flowTime = 1f;
    [SerializeField] private RainbowStripe[] stripes;

    private bool isPlaying;
    private bool isFlowing;

    private float waveCyclicTime;
    private float currentFlowTime;
    private float protrusion;

    public void Play()
    {
        isPlaying = true;
        isFlowing = true;
    }

    public void Stop()
    {
        isPlaying = false;
        isFlowing = false;

        waveCyclicTime = 0f;
        currentFlowTime = 0f;
        protrusion = 0f;

        UpdateStripes();
    }

    private void Update()
    {
        if (isPlaying)
        {
            UpdateFlow();
            UpdateWaveMotion();
        }
    }

    private void UpdateFlow()
    {
        if (isFlowing)
        {
            currentFlowTime += Time.unscaledDeltaTime;

            if (currentFlowTime >= flowTime)
            {
                currentFlowTime = flowTime;

                isFlowing = false;
            }

            protrusion = Mathf.InverseLerp(0f, flowTime, currentFlowTime);
            protrusion = Easing.QuadEaseOut(protrusion);
        }
    }

    private void UpdateWaveMotion()
    {
        waveCyclicTime += Time.unscaledDeltaTime;
        
        if (waveCyclicTime >= 1f){
            waveCyclicTime = Time.unscaledDeltaTime;
        }

        UpdateStripes();
    }

    private void UpdateStripes()
    {
        float defaultOffset = Mathf.Lerp(DEFAULT_OFFSET.MIN, DEFAULT_OFFSET.MAX, protrusion);

        foreach (RainbowStripe stripe in stripes){
            stripe.UpdateLength(waveCyclicTime, defaultOffset, valueMultiplier);
        }
    }
}
