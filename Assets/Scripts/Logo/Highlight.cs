using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private HighlightData data;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Vector3 deviation;
    private Vector3 targetPosition;

    private float currentTime;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        UpdateDeviation();
    }

    private void Update()
    {
        
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, targetPosition, data.changeSpeed * Time.unscaledDeltaTime);

        currentTime += Time.unscaledDeltaTime;

        if (currentTime >= data.switchTime)
        {
            currentTime -= data.switchTime;
            UpdateDeviation();
        }
    }

    private void UpdateDeviation()
    {
        deviation = data.deviationRange * Rand.SignedRange1();
        targetPosition = originalPosition + deviation;
    }
}
