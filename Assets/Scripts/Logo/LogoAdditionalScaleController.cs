using UnityEngine;

public class LogoAdditionalScaleController : MonoBehaviour
{
    [SerializeField] private TemporalScaleCurve firstTemporalScale;
    [SerializeField] private TemporalScaleCurve secondTemporalScale;

    private RectTransform rectTransform;
    private TemporalScaleCurve currentTemporalScale;
    private float time;
    private float lerp;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (currentTemporalScale != null)
        {
            time += Time.unscaledDeltaTime;
            lerp = Mathf.InverseLerp(0f, currentTemporalScale.upscaleTime, time);

            rectTransform.localScale = currentTemporalScale.upscaleCurve.Evaluate(lerp).ToVector3();

            if (time >= currentTemporalScale.upscaleTime)
            {
                currentTemporalScale = null;
            }
        }
    }
}
