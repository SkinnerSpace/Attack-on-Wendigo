using UnityEngine;

public class RainbowShadowConnector : MonoBehaviour
{
    [SerializeField] private RainbowShadowController controller;
    [SerializeField] private RectTransform host;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.position = host.position + controller.adjustedOffset;
    }
}
