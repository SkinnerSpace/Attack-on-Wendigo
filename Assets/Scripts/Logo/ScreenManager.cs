using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static class defaultResolution
    {
        public const float WIDTH = 825f;
        public const float HEIGHT = 550f;
    }

    private Vector2 res;
    private Vector2 difference;

    public static ScreenManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        res = new Vector2(Screen.width, Screen.height);
        difference = new Vector2(res.x / defaultResolution.WIDTH, res.y / defaultResolution.HEIGHT);
    }

    public Vector3 AdjustOffset(Vector3 oldOffset)
    {
        Vector3 adjustedOffset = new Vector3(oldOffset.x * difference.x, oldOffset.y * difference.y, oldOffset.z);
        return adjustedOffset;
    }

    public float AdjutVerticalOffset(float oldVerticalOffset)
    {
        float adjustedVerticalOffset = oldVerticalOffset * difference.y;
        return adjustedVerticalOffset;
    }
}