using UnityEngine;
using UnityEditor;

public class RainbowController : MonoBehaviour
{
    private static class DEFAULT_OFFSET
    {
        public static float MIN = 350f;
        public static float MAX = -150f;
    }

    [SerializeField] private float valueMultiplier = 64f;
    [SerializeField] private float targetFlowTime = 1f;
    [SerializeField] private RainbowStripe[] stripes;

    private static bool isPlaying;
    private static bool isFlowing;

    private static (float current, float start) waveCyclicTime;
    private static (float current, float start) flowTime;
    private float protrusion;

    public void Play()
    {
        isPlaying = true;
        isFlowing = true;

        waveCyclicTime.current = 0f;
        waveCyclicTime.start = Time.realtimeSinceStartup;

        flowTime.current = 0f;
        flowTime.start = Time.realtimeSinceStartup;
    }

    public void Stop()
    {
        isPlaying = false;
        isFlowing = false;

        waveCyclicTime.current = 0f;
        flowTime.current = 0f;
        protrusion = 0f;

        ResetStripes();
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
            flowTime.current = Time.realtimeSinceStartup - flowTime.start;

            if (flowTime.current >= targetFlowTime)
            {
                flowTime.current = targetFlowTime;
                isFlowing = false;
            }

            protrusion = Mathf.InverseLerp(0f, targetFlowTime, flowTime.current);
            protrusion = Easing.QuadEaseOut(protrusion);
        }
    }

    private void UpdateWaveMotion()
    {
        waveCyclicTime.current = Time.realtimeSinceStartup - waveCyclicTime.start;
        
        if (waveCyclicTime.current >= 1f){
            waveCyclicTime.current -= 1f;
            waveCyclicTime.start = Time.realtimeSinceStartup;
        }

        UpdateStripes();
    }

    private void UpdateStripes()
    {
        float defaultOffset = Mathf.Lerp(DEFAULT_OFFSET.MIN, DEFAULT_OFFSET.MAX, protrusion);

        foreach (RainbowStripe stripe in stripes){
            stripe.UpdateLength(waveCyclicTime.current, defaultOffset, valueMultiplier);
        }
    }

    private void ResetStripes()
    {
        foreach (RainbowStripe stripe in stripes){
            stripe.ResetLength();
        }
    }
}
