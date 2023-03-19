using UnityEngine;

public class LaserBeamFader : MonoBehaviour
{
    private static int fadeInProp = Shader.PropertyToID("BeamFadeIn");
    private static int fadeOutProp = Shader.PropertyToID("BeamFadeOut");

    private enum FadeStates
    {
        Off,
        FadeIn,
        FadeOut
    }

    [Header("Required Components")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private Chronos chronos;

    [Header("Settings")]
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;

    private MaterialPropertyBlock propBlock;

    private FadeStates fadeState;

    private float currentFadeInTime;
    private float fadeInValue;

    private float currentFadeOutTime;
    private float fadeOutValue;

    private void Awake() => propBlock = new MaterialPropertyBlock();

    public void FadeIn() => fadeState = FadeStates.FadeIn;
    public void FadeOut() => fadeState = FadeStates.FadeOut;

    public void ResetFade(){
        fadeState = FadeStates.Off;
        currentFadeInTime = 0f;
        fadeInValue = 0f;
        fadeOutValue = 0f;

        SetPropertyValue(fadeInProp, fadeInValue);
        SetPropertyValue(fadeOutProp, fadeOutValue);
    }

    public void Update()
    {
        switch (fadeState)
        {
            case FadeStates.FadeIn:
                UpdateFadeIn(); break;

            case FadeStates.FadeOut:
                UpdateFadeOut(); break;
        }
    }

    private void UpdateFadeIn()
    {
        currentFadeInTime += chronos.DeltaTime;
        StopFadeInOnTimeOut();

        fadeInValue = Mathf.InverseLerp(0f, fadeInTime, currentFadeInTime);
        SetPropertyValue(fadeInProp, fadeInValue);
    }

    private void StopFadeInOnTimeOut()
    {
        if (currentFadeInTime >= fadeInTime)
        {
            currentFadeInTime = fadeInTime;
            fadeState = FadeStates.Off;
        }
    }

    private void UpdateFadeOut()
    {
        currentFadeOutTime += chronos.DeltaTime;
        StopFadeOutOnTimeOut();

        fadeOutValue = Mathf.InverseLerp(0f, fadeOutTime, currentFadeOutTime);
        SetPropertyValue(fadeOutProp, fadeOutValue);
    }

    private void StopFadeOutOnTimeOut()
    {
        if (currentFadeOutTime >= fadeOutTime)
        {
            currentFadeOutTime = fadeOutTime;
            fadeState = FadeStates.Off;
        }
    }

    private void SetPropertyValue(int property, float value)
    {
        line.GetPropertyBlock(propBlock);
        propBlock.SetFloat(property, value);
        line.SetPropertyBlock(propBlock);
    }
}
