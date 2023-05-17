using UnityEditor;
using UnityEngine;

public class LogoPushController : MonoBehaviour
{
    [SerializeField] private RectTransform[] elements;
    [SerializeField] private RectTransform[] secondaryElements;
    [SerializeField] private float playTime = 0.3f;
    [SerializeField] private AnimationCurve curve;

    private float time;
    private bool isPlaying;
    private bool secondaryElementsAreUsed;

    private void Update()
    {
        if (isPlaying){
            UpdateScale();
        }
    }

    public void Push()
    {
        time = 0f;
        isPlaying = true;
        secondaryElementsAreUsed = false;
    }

    public void PushSecondaryElements()
    {
        time = 0f;
        isPlaying = true;
        secondaryElementsAreUsed = true;
    }

    private void UpdateScale()
    {
        time += Time.unscaledDeltaTime;

        if (time >= playTime){
            time = playTime;
            isPlaying = false;
        }

        float curvePosition = Mathf.InverseLerp(0f, playTime, time);
        float value = curve.Evaluate(curvePosition);
        Vector3 scale = new Vector3(value, value, 1f);

        UpdateRectTransforms(secondaryElementsAreUsed ? secondaryElements : elements, scale);
    }

    private void UpdateRectTransforms(RectTransform[] rectTransforms, Vector3 scale)
    {
        foreach (RectTransform rectTransform in rectTransforms)
        {
            rectTransform.localScale = scale;
        }
    }
}
