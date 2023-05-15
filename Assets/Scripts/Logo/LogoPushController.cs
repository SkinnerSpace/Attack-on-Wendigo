using UnityEditor;
using UnityEngine;

public class LogoPushController : MonoBehaviour
{
    [SerializeField] private RectTransform[] elements;
    [SerializeField] private float playTime = 0.3f;
    [SerializeField] private AnimationCurve curve;

    private static (float start, float current) time;
    private static bool isPlaying;

    private void Update()
    {
        if (isPlaying){
            UpdateScale();
        }
    }

    public void Push()
    {
        time.current = 0f;
        time.start = Time.realtimeSinceStartup;
        isPlaying = true;
    }

    private void UpdateScale()
    {
        time.current = Time.realtimeSinceStartup - time.start;

        if (time.current >= playTime){
            time.current = playTime;
            isPlaying = false;
        }

        float curvePosition = Mathf.InverseLerp(0f, playTime, time.current);
        float value = curve.Evaluate(curvePosition);
        Vector3 scale = new Vector3(value, value, 1f);

        foreach (RectTransform element in elements)
        {
            element.localScale = scale;
        }
    }
}
