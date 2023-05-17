using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowStripe : MonoBehaviour
{
    private const float DEFAULT_VERTICAL_POSITION = 300f;

    [SerializeField] private RectTransform stripeImage;
    [SerializeField] private RainbowCurve curve;

    [Range(0f,1f)]
    [SerializeField] private float timeOffset;

    public void UpdateLength(float generalTime, float defaultVerticalOffset, float valueMultiplier)
    {
        float time = generalTime + timeOffset;

        if (time > 1f){
            time -= 1f;
        }

        float yPosition = defaultVerticalOffset - (curve.Evaluate(time) * valueMultiplier);
        stripeImage.anchoredPosition = new Vector2(stripeImage.anchoredPosition.x, yPosition);
    }

    public void ResetLength()
    {
        stripeImage.anchoredPosition = new Vector2(stripeImage.anchoredPosition.x, DEFAULT_VERTICAL_POSITION);
    }
}
