using UnityEngine;

public class ShadowConnector : MonoBehaviour
{
    [SerializeField] private RectTransform host;
    [SerializeField] private Vector3 offset;
    private RectTransform rectTransform;

    private ScreenManager screenManager;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        screenManager = ScreenManager.Instance;
    }

    private void Update()
    {
        rectTransform.position = host.position;// host.position + screenManager.AdjustOffset(offset);
    }
}

