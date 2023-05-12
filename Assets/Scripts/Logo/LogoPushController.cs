using UnityEngine;

[ExecuteAlways]
public class LogoPushController : MonoBehaviour
{
    [SerializeField] private RectTransform[] elements;
    [SerializeField] private float playTime = 0.3f;
    [SerializeField] private AnimationCurve curve;

    private float time;
    private bool isPlaying;

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

        foreach (RectTransform element in elements)
        {
            element.localScale = scale;
        }
    }
}
