using UnityEngine;

public class RainbowShadowController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public Vector3 adjustedOffset { get; private set; }

    private ScreenManager screenManager;

    private void Start()
    {
        screenManager = ScreenManager.Instance;
    }

    private void Update()
    {
        //float vertical = screenManager.AdjutVerticalOffset(offset.y);
        adjustedOffset = screenManager.AdjustOffset(offset);// new Vector3(offset.x, vertical, 0f);
    }

}
